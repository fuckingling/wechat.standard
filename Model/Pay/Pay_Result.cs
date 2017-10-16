namespace WeChat.Standard.Model.Pay
{
    public class Pay_Result
    {
        public Pay_Result()
        {
            this.sign_type = "md5";
        }
        /// <summary>
        /// 返回状态码 SUCCESS/FAIL 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息，如非空，为错误原因
        /// </summary>
        public string return_msg { get; set; }
        //--------------以下字段在return_code为SUCCESS的时候有返回
        /// <summary>
        /// 公众账号ID 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 设备号 微信支付分配的终端设备号，
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 随机字符串 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 签名类型 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误返回的信息描述
        /// </summary>
        public string err_code_des { get; set; }
        /// <summary>
        /// 用户标识 用户在商户appid下的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 是否关注公众账号 用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        public string is_subscribe { get; set; }
        /// <summary>
        /// 交易类型 JSAPI、NATIVE、APP
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 付款银行 银行类型，采用字符串类型的银行标识，
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 订单金额 订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// 应结订单金额 应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额。
        /// </summary>
        public string settlement_total_fee { get; set; }
        /// <summary>
        /// 货币种类 货币类型，符合ISO4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public string cash_fee { get; set; }
        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        public string cash_fee_type { get; set; }
        /// <summary>
        /// 总代金券金额
        /// </summary>
        public string coupon_fee { get; set; }
        /// <summary>
        /// 代金券使用数量
        /// </summary>
        public string coupon_count { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商家数据包 商家数据包，原样返回
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 支付完成时间 支付完成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        public string time_end { get; set; }
    }
}
