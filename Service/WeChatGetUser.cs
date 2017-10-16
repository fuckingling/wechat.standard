using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Standard.Base;
using WeChat.Standard.Common;
using WeChat.Standard.Extend;
using WeChat.Standard.IService;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Self;

namespace WeChat.Standard.Service
{
    public class WeChatGetUser : IWeChatGetUser
    {
        /// <summary>
        /// 公众号信息
        /// </summary>
        private WeChatInfo _info;
        /// <summary>
        /// 当前上下文
        /// </summary>
        private HttpContext _context;
        private ILogger _logger;
        public WeChatGetUser(IHttpContextAccessor contextAccessor, ILoggerFactory loggerfactory)
        {
            _context = contextAccessor.HttpContext;
            _logger = loggerfactory.CreateLogger<WeChatGetMessage>();
        }
        IWeChatGetUser IWeChatGetUser.init(WeChatInfo info)
        {

            if (_info == null)
            {
                _info = info;
            }
            return this;
        }
        WeChatUser IWeChatGetUser.getUser(string openid)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            try
            {
                var access_token = BaseClass.getAccessToken(_info);
                var url = $"https://api.weixin.qq.com/cgi-bin/user/info?access_token={access_token}&openid={openid}&lang=zh_CN";
                var str = BaseClass.getFormWechatService(url);
                var user = JsonHelper.jsonToModel<WeChatUser>(str);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{this.GetType().Name}:{ex.Message}");
            }
        }

        WeChatUser IWeChatGetUser.getUserWithAuth(string code)
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
                    //获取用户信息
                    var urlforuser = $"https://api.weixin.qq.com/sns/userinfo?access_token={authAccesstoken.access_token}&openid={authAccesstoken.openid}&lang=zh_CN";
                    var user = JsonHelper.jsonToModel<WeChatUser>(BaseClass.getFormWechatService(urlforuser));
                    return user;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
            return null;
        }
        string IWeChatGetUser.getOpenidWithAuth(string code)
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
