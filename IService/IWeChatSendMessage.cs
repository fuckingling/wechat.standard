using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Send;

namespace WeChat.Standard.IService
{
    public interface IWeChatSendMessage
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IWeChatSendMessage init(WeChatInfo info);
        #region 发送消息
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool modelMsg(ModelMessage msg);
        /// <summary>
        /// 异步发送模板消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<bool> modelMsgAsync(ModelMessage msg);
        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="ToUser"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        void newsMsg(string ToUser, List<ResponseItem> list);
        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        void textMsg(string touser, string content);
        #endregion
    }
}
