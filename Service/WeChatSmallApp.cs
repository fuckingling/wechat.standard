using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WeChat.Standard.Base;
using WeChat.Standard.Common;
using WeChat.Standard.Extend;
using WeChat.Standard.IService;
using WeChat.Standard.Model;
using WeChat.Standard.Model.App;

namespace WeChat.Standard.Service
{
    public class WeChatSmallApp : IWeChatSmallApp
    {
        /// <summary>
        /// 公众号信息
        /// </summary>
        private WeChatInfo _info;
        public WeChatSmallApp()
        {
        }

        SmallApp_KeyInfo IWeChatSmallApp.GetKeyInfo(string code)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={_info.smallapp_appid}&secret={_info.smallapp_secret}&js_code={code}&grant_type=authorization_code";
            var key = JsonHelper.jsonToModel<SmallApp_KeyInfo>(BaseClass.getFormWechatService(url));
            return key;
        }

        SmallApp_UserInfo IWeChatSmallApp.GetUserInfo(SmallApp_InfoData data, string session_key)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            //签名验证
            var sign = StringEncyptionHelper.sha1(data.rawData + session_key);
            if (sign.ToLower() != data.signature.ToLower())
            {
                throw new WeChatError("签名验证不通过");
            }

            //解密开始
            try
            {
                var user = JsonHelper.jsonToModel<SmallApp_UserInfo>(StringEncyptionHelper.aesDecrypt(data.encryptedData, session_key, data.iv, CipherMode.CBC));
                //水印验证
                if (user.watermark.appid != _info.smallapp_appid)
                {
                    throw new Exception("水印验证不通过");
                }
                return user;
            }
            catch
            {

                throw new Exception("解密失败");
            }
        }

        IWeChatSmallApp IWeChatSmallApp.init(WeChatInfo info)
        {
            if (_info == null)
            {
                _info = info;
            }
            return this; ;
        }
    }
}
