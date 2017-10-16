using System;
using Newtonsoft.Json;
namespace WeChat.Standard.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 将model序列号成json
        /// </summary>
        /// <param name="t">model</param>
        /// <returns></returns>
        public static string modelToJson<T>(T t) where T : class
        {
            try
            {
                return JsonConvert.SerializeObject(t);
            }
            catch
            {
                throw new Exception("序列化失败");
            }
        }
        /// <summary>
        /// 将json反序列化成model
        /// </summary>
        /// <param name="str">json</param>
        /// <returns></returns>
        public static T jsonToModel<T>(string str) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                throw new Exception("反序列化失败");
            }
        }
    }
}