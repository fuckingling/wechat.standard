
namespace WeChat.Standard.Model.Self
{
    internal class ErrorMessage
    {
        public ErrorMessage()
        {
            this.errcode = 0;
        }
        public int errcode { set; get; }
        public string errmsg { set; get; }
        public int msgid { get; set; }
    }
}
