using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Receive;

namespace WeChat.Standard.IService
{
    public delegate void messageResponse<T>(T t);
    public interface IWeChatGetMessage
    {
        /// <summary>
        /// 初始化
        /// </summary>
        IWeChatGetMessage init(WeChatInfo info);
        
        /// <summary>
        /// 基础消息,这里可操作只要有消息进来就执行 response
        /// </summary>
        /// <param name="response"></param>
        void baseMsg(messageResponse<ReceiveMsg> response);

        #region 接收普通消息
        /// <summary>
        /// 接收文本消息
        /// </summary>
        /// <returns></returns>
        void textMsg(messageResponse<ReceiveMsgWithText> response);
        /// <summary>
        /// 接收图片消息
        /// </summary>
        /// <returns></returns>
        void imageMsg(messageResponse<ReceiveMsgWithImage> response);
        /// <summary>
        /// 接收位置消息
        /// </summary>
        /// <returns></returns>
        void locationMsg(messageResponse<ReceiveMsgWithLocation> response);
        /// <summary>
        /// 接收链接消息
        /// </summary>
        /// <returns></returns>
        void urlMsg(messageResponse<ReceiveMsgWithUrl> response);
        /// <summary>
        /// 接收语音消息
        /// </summary>
        /// <returns></returns>
        void voiceMsg(messageResponse<ReceiveMsgWithVoice> response);
        /// <summary>
        /// 接收视频消息,包含小视频
        /// </summary>
        /// <returns></returns>
        void videoMsg(messageResponse<ReceiveMsgWithVideo> response);
        #endregion

        #region 接收事件消息
        /// <summary>
        /// 点击菜单事件
        /// </summary>
        /// <param name="key">事件KEY值</param>
        /// <returns></returns>
        void clickMenuMsg(messageResponse<ReceiveEventWithClickMenu> response);
        /// <summary>
        /// 点击菜单上的链接事件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        void viewMenuMsg(messageResponse<ReceiveEventWithClickMenu> response);
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        /// <returns></returns>
        void uploadLocationMsg(messageResponse<ReceiveEventWithLocation> response);
        /// <summary>
        /// 扫描带参数的二维码事件
        /// </summary>
        /// <param name="str">模糊匹配参数</param>
        /// <returns></returns>
        void scanningQRcodeMsg(messageResponse<ReceiveEventWithQRcode> response);
        /// <summary>
        /// 关注事件
        /// </summary>
        /// <returns></returns>
        void subscribeMsg(messageResponse<ReceiveEventBase> response);
        /// <summary>
        /// 取消关注事件
        /// </summary>
        /// <returns></returns>
        void unSubscribeMsg(messageResponse<ReceiveEventBase> response);
        /// <summary>
        /// 模板消息通知
        /// </summary>
        /// <param name="response"></param>
        void modelMsgNotify(messageResponse<ReceiveEventWithModelMsg> response);
        #endregion

    }
}
