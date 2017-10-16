using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;

namespace WeChat.Standard.Common
{
    internal class XmlHelper
    {
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">json字符串</param>
        /// <returns></returns>
        public static T deserialize<T>(string str) where T : class
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("传入参数为空");
            }

            T t = null;
            try
            {
                var list = (typeof(T)).ToString().Split('.');
                var typeName = list[list.Length - 1];
                str = str.Replace("<xml>", string.Format("<{0}>", typeName));
                str = str.Replace("</xml>", string.Format("</{0}>", typeName));
                using (StringReader sr = new StringReader(str))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    t = xmldes.Deserialize(sr) as T;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"反序列化失败:{e.Message}");
            }
            return t;
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string serialize<T>(T t) where T : class
        {
            if (t == null)
            {
                throw new Exception("空模型");
            }

            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));
            try
            {
                //序列化对象
                xml.Serialize(Stream, t);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            var list = (typeof(T)).ToString().Split('.');
            var typeName = list[list.Length - 1];
            str = str.Replace(string.Format("<{0} xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">", typeName), "<xml>");
            str = str.Replace(string.Format("<{0} xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">", typeName), "<xml>");
            str = str.Replace(string.Format("</{0}>", typeName), "</xml>");
            str = str.Replace("<?xml version=\"1.0\"?>", "");
            //释放
            sr.Dispose();
            Stream.Dispose();

            return str;
        }
        /// <summary>
        /// xml序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string serializeForWechat<T>(T t) where T : class
        {
            PropertyInfo[] propertys = t.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo pi in propertys)
            {
                var name = pi.Name;
                var value = pi.GetValue(t);
                if (value == null)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    continue;
                }
                var type = value.GetType();
                if (type == typeof(string))
                {
                    var str = $"<{name}><![CDATA[{value.ToString()}]]></{name}>";
                    sb.Append(str);
                    continue;
                }
                else if (type == typeof(decimal))
                {
                    var str = $"<{name}>{Convert.ToDecimal(value)}</{name}>";
                    sb.Append(str);
                    continue;
                }
                else if (type == typeof(int))
                {
                    var str = $"<{name}>{Convert.ToInt32(value)}</{name}>";
                    sb.Append(str);
                    continue;
                }
                else if (type == typeof(byte))
                {
                    var str = $"<{name}>{Convert.ToByte(value)}</{name}>";
                    sb.Append(str);
                    continue;
                }
                else if (type == typeof(bool))
                {
                    var str = $"<{name}>{Convert.ToBoolean(value)}</{name}>";
                    sb.Append(str);
                    continue;
                }
                else
                {
                    var strIn = serializeForWechat(value).Replace("<xml>", "").Replace("</xml>", "");
                    var str = $"<{name}>{strIn}</{name}>";
                    sb.Append(str);
                    continue;
                }
            }
            //
            var result = $"<xml>{sb.ToString()}</xml>";
            return result;
        }
    }
}
