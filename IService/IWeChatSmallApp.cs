using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Standard.Model;
using WeChat.Standard.Model.App;

namespace WeChat.Standard.IService
{
    public interface IWeChatSmallApp
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IWeChatSmallApp init(WeChatInfo info);
        /// <summary>
        /// 根据小程序回传的CODE获取session_key
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        SmallApp_KeyInfo GetKeyInfo(string code);
        /// <summary>
        /// 根据小程序传回的值解密参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        SmallApp_UserInfo GetUserInfo(SmallApp_InfoData data, string session_key);
    }
}
