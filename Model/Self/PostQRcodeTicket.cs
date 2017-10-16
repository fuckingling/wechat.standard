
namespace WeChat.Standard.Model.Self
{
    internal class PostQRcodeTicket
    {

        /// <summary>
        /// 申请二维码票据
        /// </summary>
        public class QRcodeTicket
        {

            public string action_name { get; set; }
            public Action_Info action_info { get; set; }
        }
        public class QRcodeTicketWithTime : QRcodeTicket
        {
            public int expire_seconds { get; set; }
        }
        public class Action_Info
        {
            public Scene scene { get; set; }
        }

        public class Scene
        {
            public int scene_id { get; set; }
        }
    }
}
