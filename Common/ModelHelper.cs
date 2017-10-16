using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WeChat.Standard.Common
{
    public class ModelHelper
    {
        /// <summary>
        /// 模型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T convertToNewObject<T>(object obj) where T : new()
        {
            T t = new T();
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                //没给值的不导出
                try
                {
                    var tproper = t.GetType().GetProperties();
                    var p = tproper.Where(x => x.Name.ToLower() == pi.Name.ToLower()).FirstOrDefault();
                    if (p != null)
                    {
                        var ob = pi.GetValue(obj);
                        var type = ob.GetType();
                        if (type == typeof(string))
                        {
                            var str = ob.ToString();
                            p.SetValue(t, str);
                        }
                        else
                        if (type == typeof(decimal))
                        {
                            var str = Convert.ToDecimal(ob);
                            p.SetValue(t, str);
                        }
                        else
                        if (type == typeof(int))
                        {
                            var str = Convert.ToInt32(ob);
                            p.SetValue(t, str);
                        }
                        else
                        if (type == typeof(byte))
                        {
                            var str = Convert.ToByte(ob);
                            p.SetValue(t, str);
                        }
                        else
                        {
                            p.SetValue(t, ob);
                        }

                    }
                    else
                    {
                        continue;
                    }

                }
                catch
                {
                    continue;
                }
            }
            return t;
        }
        /// <summary>
        /// 将对象转换成字典,对象要求没有子对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<string, string> convertToDictionary<T>(T t) where T : class
        {
            var dic = new Dictionary<string, string>();
            if (t != null)
            {
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (var pi in propertys)
                {
                    var key = pi.Name;
                    if (pi.GetValue(t) == null)
                    {
                        continue;
                    }
                    var value = pi.GetValue(t).ToString();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }
                    dic.Add(key, value);
                }
            }
            return dic;
        }
        /// <summary>
        /// 将对象转换成链接参数 &
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string convertToUrlParameter<T>(T t) where T : class
        {
            var str = "";
            if (t != null)
            {
                PropertyInfo[] propertys = t.GetType().GetProperties().OrderBy(x => x.Name).ToArray();

                foreach (var pi in propertys)
                {
                    var key = pi.Name;
                    if (pi.GetValue(t) == null)
                    {
                        continue;
                    }
                    var value = pi.GetValue(t).ToString();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }
                    str += $"{key}={value}&";
                }
            }
            return str.Trim('&');
        }

        /// <summary>
        /// url参数转字典
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Dictionary<string, string> urlParameterToDictionary(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return null;
            }
            //去掉问号
            var p = param.TrimStart('?');
            //双连接符转单连接符
            p = p.Replace("&&", "&");
            var array = p.Split('&');
            if (array.Count() == 0)
            {
                return null;
            }
            var dic = new Dictionary<string, string>();
            foreach (var item in array)
            {
                var key = "";
                var value = "";
                var str = item.ToString().Split('=');
                if (str.Count() == 0)
                {
                    continue;
                }
                key = str[0].ToString();
                if (str.Count() == 2)
                {
                    value = str[1].ToString();
                }
                dic.Add(key, value);
            }
            return dic;
        }
    }
}
