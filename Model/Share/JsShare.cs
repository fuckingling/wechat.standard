
namespace WeChat.Standard.Model.Share
{
    public class JsShare
    {
        /// <summary>
        /// appID
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public object timestamp { get; set; }
        /// <summary>
        /// 随即字符串
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
    }
}
