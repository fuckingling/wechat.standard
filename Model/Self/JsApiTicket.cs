
namespace WeChat.Standard.Model.Self
{
    internal class JsApiTicket
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 缓存时间
        /// </summary>
        public int expires_in { get; set; }
    }
}
