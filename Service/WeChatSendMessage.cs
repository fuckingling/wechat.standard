using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeChat.Standard.Base;
using WeChat.Standard.Common;
using WeChat.Standard.Extend;
using WeChat.Standard.IService;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Self;
using WeChat.Standard.Model.Send;

namespace WeChat.Standard.Service
{
    public class WeChatSendMessage : IWeChatSendMessage
    {
        /// <summary>
        /// 公众号信息
        /// </summary>
        private WeChatInfo _info;
        /// <summary>
        /// 当前上下文
        /// </summary>
        private HttpContext _context;
        public WeChatSendMessage(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext;
        }
        IWeChatSendMessage IWeChatSendMessage.init(WeChatInfo info)
        {
            if (_info == null)
            {
                _info = info;
            }
            return this;
        }

        bool IWeChatSendMessage.modelMsg(ModelMessage msg)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            var access_token = BaseClass.getAccessToken(_info);
            var url = $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={access_token}";
            var data = JsonHelper.modelToJson(msg).Replace("\"{","{").Replace("}\"","}").Replace(@"\","");

            var str = BaseClass.postFormWechatService(data, url);
            var message = JsonHelper.jsonToModel<ErrorMessage>(str);
            return message.errcode == 0;
        }

        async Task<bool> IWeChatSendMessage.modelMsgAsync(ModelMessage msg)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            return await Task.Run<bool>(() =>
            {
                var access_token = BaseClass.getAccessToken(_info);
                var url = $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={access_token}";
                var data = JsonHelper.modelToJson(msg).Replace("\"{", "{").Replace("}\"", "}").Replace(@"\", "");

                var str = BaseClass.postFormWechatService(data, url);
                var message = JsonHelper.jsonToModel<ErrorMessage>(str);
                return message.errcode == 0;
            });
        }

        void IWeChatSendMessage.newsMsg(string ToUser, List<ResponseItem> list)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            if (list.Count > 10)
            {
                throw new WeChatError("图文消息不能超过10条");
            }
            ResponsNewsMsg news = new ResponsNewsMsg()
            {
                FromUserName = _info.rawid,
                ToUserName = ToUser,
                MsgType = "news",
                CreateTime = (uint)StringHelper.convertDateTimeInt(DateTime.Now),
                ArticleCount = list.Count,
                Articles = list
            };
            var str = XmlHelper.serialize(news);
            _context.Response.WriteAsync(str);
        }

        void IWeChatSendMessage.textMsg(string touser, string content)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            ResponseTextMsg testMsg = new ResponseTextMsg()
            {
                FromUserName = _info.rawid,
                ToUserName = touser,
                MsgType = "text",
                CreateTime = (uint)StringHelper.convertDateTimeInt(DateTime.Now),
                Content = content
            };
            var str = XmlHelper.serialize(testMsg);
            _context.Response.WriteAsync(str);
        }
    }
}
