namespace WeChat.Standard.Model.Receive
{
    public class ReceiveMsg
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public uint CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
    }

    #region 普通消息
    /// <summary>
    /// 接收消息
    /// </summary>
    public class ReceiveMsgBase : ReceiveMsg
    {
        public ulong MsgId { get; set; }
    }
    /// <summary>
    /// 接收文本消息 MsgType=text
    /// </summary>
    public class ReceiveMsgWithText : ReceiveMsgBase
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content { get; set; }
    }
    /// <summary>
    /// 接收图片消息 MsgType=image
    /// </summary>
    public class ReceiveMsgWithImage : ReceiveMsgBase
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
    }
    /// <summary>
    /// 接收语音消息 MsgType=voice
    /// </summary>
    public class ReceiveMsgWithVoice : ReceiveMsgBase
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 开启语音识别后会多这么个东西,应该是语音识别内容
        /// </summary>
        public string Recognition { get; set; }
    }
    /// <summary>
    /// 接收视频消息，包含小视频消息 MsgType=video/ MsgType=shortvideo
    /// </summary>
    public class ReceiveMsgWithVideo : ReceiveMsgBase
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
    /// <summary>
    /// 接收位置消息 MsgType=location
    /// </summary>
    public class ReceiveMsgWithLocation : ReceiveMsgBase
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public decimal Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public decimal Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public uint Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }
    /// <summary>
    /// 接收链接消息 MsgType=link
    /// </summary>
    public class ReceiveMsgWithUrl : ReceiveMsgBase
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public uint Url { get; set; }
    }
    #endregion

    #region 事件消息
    /// <summary>
    /// 接收事件,关注/取消关注都只有这一个,事件类型，subscribe(订阅)、unsubscribe(取消订阅)
    /// </summary>
    public class ReceiveEventBase : ReceiveMsg
    {
        public string Event { get; set; }
    }
    /// <summary>
    /// 扫描带参数二维码事件:1. 用户未关注时，进行关注后的事件推送,事件类型，subscribe.2. 用户已关注时的事件推送,事件类型，SCAN
    /// </summary>
    public class ReceiveEventWithQRcode : ReceiveEventBase
    {
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }
    /// <summary>
    /// 上报地理位置事件,事件类型，LOCATION
    /// </summary>
    public class ReceiveEventWithLocation : ReceiveEventBase
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public decimal Precision { get; set; }
    }
    /// <summary>
    /// 自定义菜单事件,事件类型，CLICK,	点击菜单跳转链接时的事件推送点击事件类型，VIEW
    /// </summary>
    public class ReceiveEventWithClickMenu : ReceiveEventBase
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应，如果是点的菜单上的链接则KEY为设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
    }
    /// <summary>
    /// 自定义菜单事件,事件类型，CLICK,	点击菜单跳转链接时的事件推送点击事件类型，VIEW
    /// </summary>
    public class ReceiveEventWithModelMsg : ReceiveEventBase
    {
        /// <summary>
        /// 状态 success 为成功 failed: system failed为其他原因发送失败  failed:user block用户拒绝
        /// </summary>
        public string Status { get; set; }
    }
    #endregion
}
