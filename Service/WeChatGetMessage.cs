using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeChat.Standard.Base;
using WeChat.Standard.Common;
using WeChat.Standard.Enum;
using WeChat.Standard.Extend;
using WeChat.Standard.IService;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Receive;

namespace WeChat.Standard.Service
{
    public class WeChatGetMessage : IWeChatGetMessage
    {
        /// <summary>
        /// 公众号信息
        /// </summary>
        private WeChatInfo _info;
        /// <summary>
        /// 当前上下文
        /// </summary>
        private HttpContext _context;
        /// <summary>
        /// 微信给过来的消息内容
        /// </summary>
        private string _wechatmsg;
        /// <summary>
        /// 基础消息->可判断消息类型
        /// </summary>
        private ReceiveMsg _basemsg;
        /// <summary>
        /// url参数
        /// </summary>
        private static Dictionary<string, string> _params;
        private ILogger _logger;
        public WeChatGetMessage(IHttpContextAccessor contextAccessor,ILoggerFactory loggerfactory)
        {
            _context = contextAccessor.HttpContext;
            _logger = loggerfactory.CreateLogger<WeChatGetMessage>();
        }
        #region 实现
        IWeChatGetMessage IWeChatGetMessage.init(WeChatInfo info)
        {

            if (_info == null)
            {
                _info = info;
                var urlvalue = _context.Request.QueryString.Value;
                _params = ModelHelper.urlParameterToDictionary(urlvalue);
                this.receiveMessages();
            }
            return this;
        }

        void IWeChatGetMessage.baseMsg(messageResponse<ReceiveMsg> response)
        {
            var model = XmlHelper.deserialize<ReceiveMsg>(_wechatmsg);
            response(model);
        }
        void IWeChatGetMessage.clickMenuMsg(messageResponse<ReceiveEventWithClickMenu> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.CLICK.ToString())
                    {
                        var model = XmlHelper.deserialize<ReceiveEventWithClickMenu>(_wechatmsg);
                        response(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.imageMsg(messageResponse<ReceiveMsgWithImage> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.image.ToString())
                {
                    var model = XmlHelper.deserialize<ReceiveMsgWithImage>(_wechatmsg);
                    response(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.locationMsg(messageResponse<ReceiveMsgWithLocation> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.location.ToString())
                {
                    var model = XmlHelper.deserialize<ReceiveMsgWithLocation>(_wechatmsg);
                    response(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.scanningQRcodeMsg(messageResponse<ReceiveEventWithQRcode> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.SCAN.ToString())
                    {
                        var model = XmlHelper.deserialize<ReceiveEventWithQRcode>(_wechatmsg);
                        if (model.EventKey.Contains("qrscene_"))
                        {
                            response(model);
                        }
                    }
                    if (baseevent.Event == EnumReceive.Event.subscribe.ToString())
                    {
                        var model = XmlHelper.deserialize<ReceiveEventWithQRcode>(_wechatmsg);
                        if (model.EventKey.Contains("qrscene_"))
                        {
                            response(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.subscribeMsg(messageResponse<ReceiveEventBase> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.subscribe.ToString())
                    {
                        response(baseevent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.textMsg(messageResponse<ReceiveMsgWithText> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.text.ToString())
                {
                    var model = XmlHelper.deserialize<ReceiveMsgWithText>(_wechatmsg);
                    response(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.unSubscribeMsg(messageResponse<ReceiveEventBase> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.unsubscribe.ToString())
                    {
                        response(baseevent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.uploadLocationMsg(messageResponse<ReceiveEventWithLocation> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.LOCATION.ToString())
                    {
                        var model = XmlHelper.deserialize<ReceiveEventWithLocation>(_wechatmsg);
                        response(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.urlMsg(messageResponse<ReceiveMsgWithUrl> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.link.ToString())
                {
                    var model = XmlHelper.deserialize<ReceiveMsgWithUrl>(_wechatmsg);
                    response(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.videoMsg(messageResponse<ReceiveMsgWithVideo> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.video.ToString() || _basemsg.MsgType == EnumReceive.MsgType.shortvideo.ToString())
                {
                    var model = XmlHelper.deserialize<ReceiveMsgWithVideo>(_wechatmsg);
                    response(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }

        void IWeChatGetMessage.viewMenuMsg(messageResponse<ReceiveEventWithClickMenu> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.VIEW.ToString())
                    {
                        var model = XmlHelper.deserialize<ReceiveEventWithClickMenu>(_wechatmsg);
                        response(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }
        void IWeChatGetMessage.modelMsgNotify(messageResponse<ReceiveEventWithModelMsg> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.@event.ToString())
                {
                    var baseevent = XmlHelper.deserialize<ReceiveEventBase>(_wechatmsg);
                    if (baseevent.Event == EnumReceive.Event.TEMPLATESENDJOBFINISH.ToString())
                    {
                        var model = XmlHelper.deserialize<ReceiveEventWithModelMsg>(_wechatmsg);
                        response(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }
        void IWeChatGetMessage.voiceMsg(messageResponse<ReceiveMsgWithVoice> response)
        {
            try
            {
                if (_basemsg.MsgType == EnumReceive.MsgType.voice.ToString())
                {
                    var model = XmlHelper.deserialize<ReceiveMsgWithVoice>(_wechatmsg);
                    response(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}:{ex.Message}");
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 验证微信服务器的数据
        /// </summary>
        private bool checkSign
        {
            get {
                if(_info==null)
                {
                    throw new WeChatError("请先调用init初始化");
                }
                
                if (_params == null)
                {
                    return false;
                }
                var signature = _params["signature"];
                var timestamp = _params["timestamp"]; ;
                var nonce = _params["nonce"];
                string[] array = { _info.token, timestamp, nonce };
                Array.Sort(array);
                var arrayString = string.Join("", array);
                var str = StringEncyptionHelper.sha1(arrayString).ToLower();
                if (string.Compare(str, signature.ToLower(), true) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 接收微信服务器消息
        /// </summary>
        private void receiveMessages()
        {
            if (this.checkSign)
            {
                if (_context.Request.Method.ToUpper() == "POST")
                {
                    using (Stream stream = _context.Request.Body)
                    {
                        byte[] bytes = new byte[_context.Request.ContentLength.Value];
                        stream.Read(bytes, 0, bytes.Length);
                        stream.Dispose();

                        string postString = Encoding.UTF8.GetString(bytes);
                        if (!string.IsNullOrEmpty(postString))
                        {
                            _wechatmsg = postString;
                            _basemsg = XmlHelper.deserialize<ReceiveMsg>(_wechatmsg);

                        }
                    }
                }
                else
                {
                    Encoding.GetEncoding("utf-8");
                    string echoString = _params["echostr"];
                    _context.Response.WriteAsync(echoString);

                }
            }
            else
            {
                throw new WeChatError("验签不通过");
            }
        }

        #endregion
    }
}
