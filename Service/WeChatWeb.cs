using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WeChat.Standard.Base;
using WeChat.Standard.Common;
using WeChat.Standard.Extend;
using WeChat.Standard.IService;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Self;
using WeChat.Standard.Model.Share;

namespace WeChat.Standard.Service
{
    public class WeChatWeb : IWeChatWeb
    {
        /// <summary>
        /// 公众号信息
        /// </summary>
        private WeChatInfo _info;
        private ILogger _logger;
        /// <summary>
        /// 当前上下文
        /// </summary>
        private HttpContext _context;
        public WeChatWeb(IHttpContextAccessor contextAccessor, ILoggerFactory loggerfactory)
        {
            _context = contextAccessor.HttpContext;
            _logger = loggerfactory.CreateLogger<WeChatWeb>();
        }
        IWeChatWeb IWeChatWeb.init(WeChatInfo info)
        {

            if (_info == null)
            {
                _info = info;
            }
            return this;
        }
        void IWeChatWeb.auth(string url, string stata, bool agree)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            if (!string.IsNullOrWhiteSpace(stata))
            {
                stata = WebUtility.UrlEncode(StringEncyptionHelper.aesEncrypt(stata));
            }
            var link = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={_info.appid}&redirect_uri={url}&response_type=code&scope=snsapi_userinfo&state={stata}#wechat_redirect";
            if (!agree)
            {
                link = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={_info.appid}&redirect_uri={url}&response_type=code&scope=snsapi_base&state={stata}#wechat_redirect";
            }
            _context.Response.Redirect(link);
        }

        byte[] IWeChatWeb.getImg(string url)
        {
            var bts = HttpHelper.getBytes<string>(url);
            return bts;
        }

        JsShare IWeChatWeb.getJsShareConfig(string url)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new WeChatError("Url值为空");
            }
            var jsapi_ticket = BaseClass.getJsApi_Ticket(_info);
            var timestamp = StringHelper.convertDateTimeInt(DateTime.Now);
            var noncestr = StringHelper.getRandomString(16);
            var signature = StringEncyptionHelper.sha1(string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, noncestr, timestamp, url)).ToLower();
            var jsapi = new JsShare()
            {
                appId = _info.appid,
                nonceStr = noncestr,
                signature = signature,
                timestamp = timestamp
            };
            return jsapi;
        }

        string IWeChatWeb.getQRcodeImgSrc(int scene_id, int expire_seconds)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            var access_token = BaseClass.getAccessToken(_info);
            var url = $"https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={access_token}";
            var postData = string.Empty;

            if (expire_seconds == 0)
            {
                var t = new PostQRcodeTicket.QRcodeTicket();
                var a = new PostQRcodeTicket.Action_Info();
                var s = new PostQRcodeTicket.Scene();
                s.scene_id = scene_id;
                a.scene = s;
                t.action_info = a;
                t.action_name = "QR_LIMIT_SCENE";
                postData = JsonHelper.modelToJson<PostQRcodeTicket.QRcodeTicket>(t);
            }
            else
            {
                var t = new PostQRcodeTicket.QRcodeTicketWithTime();
                var a = new PostQRcodeTicket.Action_Info();
                var s = new PostQRcodeTicket.Scene();
                t.expire_seconds = expire_seconds;
                s.scene_id = scene_id;
                a.scene = s;
                t.action_info = a;
                t.action_name = "QR_SCENE";
                postData = JsonHelper.modelToJson<PostQRcodeTicket.QRcodeTicketWithTime>(t);
            }
            string str = BaseClass.postFormWechatService(postData, url);
            QRcodeTicket qt = JsonHelper.jsonToModel<QRcodeTicket>(str);
            var realurl = $"https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={qt.ticket}";
            return realurl;
        }

        

        string IWeChatWeb.getOpenidOnly(string code)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return null;
                }
                var url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={_info.appid}&secret={_info.appsecret}&code={code}&grant_type=authorization_code";
                //获取authAccesstoken
                var str = BaseClass.getFormWechatService(url);
                var authAccesstoken = JsonHelper.jsonToModel<AuthAccessToken>(str);
                if (authAccesstoken != null)
                {
                    return authAccesstoken.openid;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
            return null;
        }
    }
}
