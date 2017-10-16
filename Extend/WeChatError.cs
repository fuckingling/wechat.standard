using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.Standard.Extend
{
    public class WeChatError : Exception
    {
        public WeChatError()
        {

        }
        public WeChatError(string message) : base(message)
        {

        }
        public WeChatError(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
