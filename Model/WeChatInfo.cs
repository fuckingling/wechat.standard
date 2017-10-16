namespace WeChat.Standard.Model
{
    public class WeChatInfo
    {
        public WeChatInfo() {
            this.pay_sign_type = "MD5";
        }
        /// <summary>
        /// 微信原始id,类似这：gh_5b31128cf722
        /// </summary>
        public string rawid { get; set; }
        /// <summary>
        /// 微信名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string appsecret { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public string encodingaeskey { get; set; }
        //-------------------------------支付相关------------------------------------
        /// <summary>
        /// 支付账户的微信ID
        /// </summary>
        public string pay_appid { get; set; }
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public string pay_mchid { get; set; }
        /// <summary>
        /// 签名算法，默认为MD5，支持HMAC-SHA256和MD5。
        /// </summary>
        public string pay_sign_type { get; set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string pay_sign_key { get; set; }
        /// <summary>
        /// SSL证书目录 注意应该填写绝对路径（仅退款、撤销订单时需要）
        /// </summary>
        public string pay_sslpath { get; set; }
        /// <summary>
        /// 证书密码
        /// </summary>
        public string pay_sslpassword { get; set; }
        /// <summary>
        /// 支付异步消息回调
        /// </summary>
        public string pay_notifyurl { get; set; }

        //-------------------------------小程序相关------------------------------------
        /// <summary>
        /// 小程序appid
        /// </summary>
        public string smallapp_appid { get; set; }
        /// <summary>
        /// 小程序secret
        /// </summary>
        public string smallapp_secret { get; set; }

    }
}
