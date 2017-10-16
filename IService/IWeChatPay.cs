using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Pay;

namespace WeChat.Standard.IService
{
    public interface IWeChatPay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IWeChatPay init(WeChatInfo info);
        /// <summary>
        /// 统一下单 trade_type=NATIVE时返回code_url 不是则返回prepay_id
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns></returns>
        string unifiedOrder(Pay_UnifiedOrder order);
        /// <summary>
        /// 支付通知,验证通过返回非空对象,通不过则返回空,所以前端直接判断是否空就行 然后再进行金额等判断
        /// </summary>
        /// <returns></returns>
        Pay_Result notifyPay();
        /// <summary>
        /// JSAPI所需求的一些参数
        /// </summary>
        /// <param name="prepay_id">统一下单接口返回的prepay_id参数值</param>
        /// <returns></returns>
        Pay_JsAPI jsPayInfo(string prepay_id);
    }
}
