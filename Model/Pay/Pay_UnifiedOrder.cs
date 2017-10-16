using System;
namespace WeChat.Standard.Model.Pay
{
    /// <summary>
    /// 统一下单
    /// </summary>
    public class Pay_UnifiedOrder
    {
        public Pay_UnifiedOrder()
        {
            this.device_info = "WEB";
            this.attach = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.fee_type = "CNY";
            this.time_start= DateTime.Now.ToString("yyyyMMddHHmmss");
            this.time_expire= DateTime.Now.AddDays(1).ToString("yyyyMMddHHmmss");
            this.limit_pay = "no_credit";
        }
        /// <summary>
        /// 公众账号ID,微信支付分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 设备号,自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB",非必填
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 随机字符串,随机字符串，长度要求在32位以内。
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 签名类型,签名类型，默认为MD5，支持HMAC-SHA256和MD5,非必填
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 商品描述 商品简单描述，该字段请按照规范传递，具体请见
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品详情,	单品优惠字段(暂未上线),非必填
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 附加数据,附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用,非必填
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 商户订单号,商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 标价币种,符合ISO 4217标准的三位字母代码，默认人民币：CNY,非必填
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 标价金额,订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// 终端IP
        /// </summary>
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 交易起始时间,订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。,非必填
        /// </summary>
        public string time_start { get; set; }
        /// <summary>
        /// 交易结束时间,订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。注意：最短失效时间间隔必须大于5分钟,非必填
        /// </summary>
        public string time_expire { get; set; }
        /// <summary>
        /// 订单优惠标记,订单优惠标记，使用代金券或立减优惠功能时需要的参数,非必填
        /// </summary>
        public string goods_tag { get; set; }
        /// <summary>
        /// 通知地址,异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 交易类型,取值如下：JSAPI，NATIVE，APP等，
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 商品ID,trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义,非必填
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 指定支付方式,上传此参数no_credit--可限制用户不能使用信用卡支付,非必填
        /// </summary>
        public string limit_pay { get; set; }
        /// <summary>
        /// 用户标识 trade_type=JSAPI时（即公众号支付），此参数必传，此参数为微信用户在商户对应appid下的唯一标识。openid如何获取，可参考【获取openid】。企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，再调用【企业号userid转openid接口】进行转换,非必填
        /// </summary>
        public string openid { get; set; }

    }
}
