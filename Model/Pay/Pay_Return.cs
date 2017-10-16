namespace WeChat.Standard.Model.Pay
{
    public class Pay_Return
    {
        public Pay_Return() {
            this.return_code = "FAIL";
            this.return_msg = "消息通知失败";
        }
        /// <summary>
        /// 成功：SUCCESS，失败：FAIL
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 成功：OK,失败：理由
        /// </summary>
        public string return_msg { get; set; }
    }
}
