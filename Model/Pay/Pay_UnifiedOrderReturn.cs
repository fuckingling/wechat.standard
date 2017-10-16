
namespace WeChat.Standard.Model.Pay
{
    /// <summary>
    /// 统一下单预支付返回结果
    /// </summary>
    internal class Pay_UnifiedOrderReturn
    {
        /// <summary>
        /// 返回状态码,SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息 返回信息，如非空，为错误原因
        /// </summary>
        public string return_msg { get; set; }
        //----------------以下字段在return_code为SUCCESS的时候有返回
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 设备号 自定义参数，可以为请求支付的终端设备号等
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }
        //---------以下字段在return_code 和result_code都为SUCCESS的时候有返回
        /// <summary>
        /// 交易类型 交易类型，取值为：JSAPI，NATIVE，APP等
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 预支付交易会话标识 微信生成的预支付会话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        public string prepay_id { get; set; }
        /// <summary>
        /// 二维码链接 trade_type为NATIVE时有返回，用于生成二维码，展示给用户进行扫码支付
        /// </summary>
        public string code_url { get; set; }
    }
}
