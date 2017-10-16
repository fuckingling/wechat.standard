using System.Collections.Generic;
using System.Xml.Serialization;

namespace WeChat.Standard.Model.Send
{
    /// <summary>
    /// 回复消息
    /// </summary>
    public class ResponseMsg
    {
        /// <summary>
        /// 接收方帐号(一个OpenID)
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 开发者微信号
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
    /// <summary>
    /// 回复文本消息 MsgType=text
    /// </summary>
    public class ResponseTextMsg : ResponseMsg
    {
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public string Content { get; set; }
    }
    /// <summary>
    /// 回复图片消息 MsgType=image
    /// </summary>
    public class ResponseImageMsg : ResponseMsg
    {
        /// <summary>
        /// 图片
        /// </summary>
        public ResponseImage Image { get; set; }
    }
    /// <summary>
    /// 回复语音消息 语音，MsgType=voice
    /// </summary>
    public class ResponseVoiceMsg : ResponseMsg
    {
        /// <summary>
        /// 语音
        /// </summary>
        public ResponseVoice Voice { get; set; }
    }
    /// <summary>
    /// 回复视频消息 视频，MsgType=video
    /// </summary>
    public class ResponseVideoMsg : ResponseMsg
    {
        /// <summary>
        /// 视频
        /// </summary>
        public ResponseVideo Video { get; set; }
    }
    /// <summary>
    /// 回复音乐消息 音乐，MsgType=music
    /// </summary>
    public class ResponseMusicMsg : ResponseMsg
    {
        /// <summary>
        /// 音乐
        /// </summary>
        public ResponseMusic Music { get; set; }
    }
    /// <summary>
    /// 回复图文消息 MsgType=news
    /// </summary>
    public class ResponsNewsMsg : ResponseMsg
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount { get; set; }
        /// <summary>
        /// 富文本
        /// </summary>
        [XmlArrayItem("item")]
        public List<ResponseItem> Articles { get; set; }
    }
    #region 外面要用
    /// <summary>
    /// 图片，Image
    /// </summary>
    public class ResponseImage
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public ulong MediaId { get; set; }
    }

    /// <summary>
    /// 语音，voice
    /// </summary>
    public class ResponseVoice
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public ulong MediaId { get; set; }
    }

    /// <summary>
    /// 语音，video
    /// </summary>
    public class ResponseVideo
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public ulong MediaId { get; set; }
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 音乐，music
    /// </summary>
    public class ResponseMusic
    {
        /// <summary>
        /// 音乐消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐消息的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicURL { get; set; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 富文本,图文内容
    /// </summary>
    public class ResponseItem
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
    }
    #endregion

}
