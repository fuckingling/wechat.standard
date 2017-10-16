using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Standard.Model;

namespace WeChat.Standard.IService
{
    public interface IWeChatGetUser
    {
        /// <summary>
        /// 根据openid获取已经关注用户的信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        WeChatUser getUser(string openid);
        /// <summary>
        /// 授权用户获得信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        WeChatUser getUserWithAuth(string code);
        /// <summary>
        /// 授权只获取openid，用于静默授权
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string getOpenidWithAuth(string code);
        /// <summary>
        /// 初始化
        /// </summary>
        IWeChatGetUser init(WeChatInfo info);
    }
}
