using System.Text;
using System.Linq;
using System.Reflection;

namespace WeChat.Standard.Model.Send
{
    public class ModelMessage
    {
        public ModelMessage(string touser,string template_id, First first, Remark remark,string url="", Miniprogram miniprogram=null,params Keywords[] keywords)
        {
            this.touser = touser;
            this.template_id = template_id;
            this.url = url;
            this.miniprogram = miniprogram;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"{" +
                "\"first\":{" +
                "\"value\":\"" + first.value + "\"," +
                "\"color\":\"" + first.color + "\"}");
            for (var i=0;i< keywords.Length;i++)
            {
                sb.Append(@"," +
                    "\"keyword" + (i+1).ToString()+"\":{" +
                    "\"value\":\"" + keywords[i].value + "\"," +
                    "\"color\":\"" + keywords[i].color + "\"}");
            }
            sb.Append(@"," +
                "\"remark\":{" +
                "\"value\":\"" + remark.value + "\"," +
                "\"color\":\"" + remark.color + "\"}" +
                "}");
            this.data=sb.ToString();
        }
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public Miniprogram miniprogram { get; set; }
        public string data { get; set; }
    }

    public class Miniprogram
    {
        public Miniprogram(string appid,string pagepath)
        {
            this.appid = appid;
            this.pagepath = pagepath;
        }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

    public class First
    {
        public First()
        {

        }
        public First(string value)
        {
            this.value = value;
            this.color = "#173177";
        }
        public First(string value, string color)
        {
            this.value = value;
            this.color = color;
        }
        public string value { get; set; }
        public string color { get; set; }
    }
    public class Remark:First
    {
        public Remark()
        {

        }
        public Remark(string value)
        {
            this.value = value;
            this.color = "#173177";
        }
        public Remark(string value, string color)
        {
            this.value = value;
            this.color = color;
        }
    }
    public class Keywords : First
    {
        public Keywords()
        {

        }
        public Keywords(string value)
        {
            this.value = value;
            this.color = "#173177";
        }
        public Keywords(string value, string color)
        {
            this.value = value;
            this.color = color;
        }
    }
}
