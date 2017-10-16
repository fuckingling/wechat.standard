using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeChat.Standard.Common;
using WeChat.Standard.Extend;
using WeChat.Standard.IService;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Pay;

namespace WeChat.Standard.Service
{
    public class WeChatPay : IWeChatPay
    {
        /// <summary>
        /// 公众号信息
        /// </summary>
        private WeChatInfo _info;
        private ILogger _logger;
        /// <summary>
        /// 当前上下文
        /// </summary>
        private HttpContext _context;
        /// <summary>
        /// 统一下单地址，除被扫支付场景以外，商户系统先调用该接口在微信支付服务后台生成预支付交易单，返回正确的预支付交易回话标识后再按扫码、JSAPI、APP等不同场景生成交易串调起支付。
        /// </summary>
        private static readonly string unifiedorderurl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        public WeChatPay(IHttpContextAccessor contextAccessor, ILoggerFactory loggerfactory)
        {
            _context = contextAccessor.HttpContext;
            _logger = loggerfactory.CreateLogger<WeChatWeb>();
        }
        IWeChatPay IWeChatPay.init(WeChatInfo info)
        {

            if (_info == null)
            {
                _info = info;
            }
            return this;
        }

        Pay_JsAPI IWeChatPay.jsPayInfo(string prepay_id)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            var payinfo = new Pay_JsAPI
            {
                appId = _info.pay_appid,
                nonceStr = StringHelper.getRandomString(16),
                timeStamp = StringHelper.convertDateTimeInt(DateTime.Now).ToString(),
                package = $"prepay_id={prepay_id}",
            };
            //参数给完 开始签名
            var str = ModelHelper.convertToUrlParameter(payinfo) + $"&key={_info.pay_sign_key}";
            payinfo.paySign = StringEncyptionHelper.md5(str).ToUpper();
            return payinfo;
        }

        Pay_Result IWeChatPay.notifyPay()
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            using (Stream stream = _context.Request.Body)
            {
                byte[] bytes = new byte[_context.Request.ContentLength.Value];
                stream.Read(bytes, 0, bytes.Length);
                stream.Dispose();
                string postString = Encoding.UTF8.GetString(bytes);
                if (string.IsNullOrEmpty(postString))
                {
                    throw new Exception("没有POST数据过来");
                }
                //序列化
                var result = XmlHelper.deserialize<Pay_Result>(postString);
                if (result == null)
                {
                    throw new Exception("支付序列化失败");
                }
                if (result.return_code != "SUCCESS")
                {
                    throw new Exception($"支付未通过:{result.return_msg}");
                }
                if (result.result_code != "SUCCESS")
                {
                    throw new Exception($"支付失败:{result.err_code}-{result.err_code_des}");
                }
                //验证
                var sign = result.sign;
                var sign_type = result.sign_type;
                result.sign = null;
                result.sign_type = null;
                var str = ModelHelper.convertToUrlParameter(result) + $"&key={_info.pay_sign_key}";
                if (sign_type.ToLower() == "md5")
                {
                    result.sign = StringEncyptionHelper.md5(str).ToUpper();
                }
                else
                {
                    result.sign = StringEncyptionHelper.hash_hmac256(str, _info.pay_sign_key);
                }
                if (sign == result.sign)
                {
                    return result;
                }
            }
            throw new Exception("签名验证失败");
        }

        string IWeChatPay.unifiedOrder(Pay_UnifiedOrder order)
        {
            if (_info == null)
            {
                throw new WeChatError("请先调用init初始化");
            }
            order.appid = _info.pay_appid;
            order.mch_id = _info.pay_mchid;
            order.sign_type = _info.pay_sign_type;
            order.notify_url = _info.pay_notifyurl;
            order.nonce_str = StringHelper.getRandomString(16);
            //参数给完 开始签名
            var str = ModelHelper.convertToUrlParameter(order) + $"&key={_info.pay_sign_key}";
            if (order.sign_type.ToLower() == "md5")
            {
                order.sign = StringEncyptionHelper.md5(str).ToUpper();
            }
            else
            {
                order.sign = StringEncyptionHelper.hash_hmac256(str, _info.pay_sign_key);
            }
            //检测必填参数
            if (string.IsNullOrWhiteSpace(order.out_trade_no))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (string.IsNullOrWhiteSpace(order.body))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (order.total_fee <= 0)
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (string.IsNullOrWhiteSpace(order.trade_type))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (order.trade_type == "JSAPI" && string.IsNullOrWhiteSpace(order.openid))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (order.trade_type.ToString() == "NATIVE" && string.IsNullOrWhiteSpace(order.product_id))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (string.IsNullOrWhiteSpace(order.notify_url))
            {
                throw new Exception("统一支付接口中，notify_url");
            }
            //开始组装xml字符串
            var xml = XmlHelper.serialize(order);
            //传值
            var response = HttpHelper.post(unifiedorderurl, xml);
            var result = XmlHelper.deserialize<Pay_UnifiedOrderReturn>(response);
            //开始判断
            if (result.return_code != "SUCCESS")
            {
                throw new Exception(result.return_msg);

            }
            if (result.result_code != "SUCCESS")
            {
                throw new Exception($"错误代码:{result.err_code},错误描述:{result.err_code_des}");
            }

            //---业务返回,只有为NATIVE时才返回二维码地址,其他时间返回prepay_id
            if (result.trade_type == "NATIVE")
            {
                return result.code_url;
            }
            return result.prepay_id;
        }
    }
}
