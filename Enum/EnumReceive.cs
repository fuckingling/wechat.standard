namespace WeChat.Standard.Enum
{
    public class EnumReceive
    {
        /// 消息类型
        /// </summary>
        public enum MsgType
        {
            /// <summary>
            /// 文本消息
            /// </summary>
            text = 0,

            /// <summary>
            /// 图片消息
            /// </summary>
            image,

            /// <summary>
            /// 语音消息
            /// </summary>
            voice,

            /// <summary>
            /// 视频消息
            /// </summary>
            video,

            /// <summary>
            /// 小视频消息
            /// </summary>
            shortvideo,

            /// <summary>
            /// 地理位置消息
            /// </summary>
            location,

            /// <summary>
            /// 链接消息
            /// </summary>
            link,

            /// <summary>
            /// 事件消息
            /// </summary>
            @event

        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public enum Event
        {
            /// <summary>
            /// 关注事件
            /// </summary>
            subscribe = 0,

            /// <summary>
            /// 取消关注事件
            /// </summary>
            unsubscribe,

            /// <summary>
            /// 扫描带参数二维码事件,用户已关注时的事件推送
            /// </summary>
            SCAN,

            /// <summary>
            /// 上报地理位置
            /// </summary>
            LOCATION,

            /// <summary>
            /// 自定义菜单事件
            /// </summary>
            CLICK,

            /// <summary>
            /// 点击菜单跳转链接时的事件推送
            /// </summary>
            VIEW,
            /// <summary>
            /// 模板消息推送返回消息
            /// </summary>
            TEMPLATESENDJOBFINISH

        }
    }
}
