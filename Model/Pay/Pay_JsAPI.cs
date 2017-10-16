
namespace WeChat.Standard.Model.Pay
{
    public class Pay_JsAPI
    {
        public Pay_JsAPI()
        {
            this.signType = "MD5";
        }
        /// <summary>
        /// 公众号ID 商户注册具有支付权限的公众号成功后即可获得
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 统一下单接口返回的prepay_id参数值，提交格式如：prepay_id=***
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 签名算法，暂支持MD5
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string paySign { get; set; }
    }
}
