using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace WeChat.Standard.Common
{
    public class HttpHelper
    {
        private static readonly HttpClient httpClient;
        public delegate void httpCallBackDelegate(string ret);
        static HttpHelper()
        {
            httpClient = new HttpClient();
        }
        /// <summary>
        /// HttpGet 同步方法获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string get<T>(string url, T t = null, Dictionary<string, string> header = null) where T : class
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }
                //链接参数
                if (t != null)
                {
                    if (url.Contains("?"))
                    {
                        url += $"&{ModelHelper.convertToUrlParameter(t)}";
                    }
                    else
                    {
                        url += $"?{ModelHelper.convertToUrlParameter(t)}";
                    }
                }
                //请求
                var ret = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
                return ret;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// HttpGet 同步方法获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] getBytes<T>(string url, T t = null, Dictionary<string, string> header = null) where T : class
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }
                //链接参数
                if (t != null)
                {
                    if (url.Contains("?"))
                    {
                        url += $"&{ModelHelper.convertToUrlParameter(t)}";
                    }
                    else
                    {
                        url += $"?{ModelHelper.convertToUrlParameter(t)}";
                    }
                }
                //请求
                var response = httpClient.GetByteArrayAsync(url).GetAwaiter();

                return response.GetResult();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 同步POST数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="t"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string post<T>(string url, T t = null, Dictionary<string, string> header = null, string encoding = "utf-8") where T : class
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("Method", "Post");
                httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }
                //将对象序列化成字典
                var dic = ModelHelper.convertToDictionary(t);


                HttpContent content = new FormUrlEncodedContent(dic);
                var response = httpClient.PostAsync(url, content).GetAwaiter().GetResult();
                var contentType = response.Content.Headers.ContentType;
                if (string.IsNullOrEmpty(contentType.CharSet))
                {
                    contentType.CharSet = encoding;
                }
                return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string post(string url, string str = null, Dictionary<string, string> header = null, string encoding = "utf-8")
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("Method", "Post");
                httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }

                HttpContent content = new StringContent(str);
                var response = httpClient.PostAsync(url, content).GetAwaiter().GetResult();
                var contentType = response.Content.Headers.ContentType;
                if (string.IsNullOrEmpty(contentType.CharSet))
                {
                    contentType.CharSet = encoding;
                }
                return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void postAsync(string url, httpCallBackDelegate httpDelegate,string str = null, Dictionary<string, string> header = null, string encoding = "utf-8")
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("Method", "Post");
                httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }

                HttpContent content = new StringContent(str);
                httpClient.PostAsync(url, content).ContinueWith((task) =>
                {
                    HttpResponseMessage response = task.Result;
                    // 确认响应成功，否则抛出异常  
                    response.EnsureSuccessStatusCode();
                    // 异步读取响应为字符串 
                    var contentType = response.Content.Headers.ContentType;
                    if (string.IsNullOrEmpty(contentType.CharSet))
                    {
                        contentType.CharSet = encoding;
                    }
                    response.Content.ReadAsStringAsync().ContinueWith(
                        (readTask) =>
                        {
                            var ret = readTask.Result;
                            //响应委托任务
                            httpDelegate(ret);
                        });
                });
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 异步POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpDelegate">委托</param>
        /// <param name="t"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        public static void postAsync<T>(string url, httpCallBackDelegate httpDelegate, T t = null, Dictionary<string, string> header = null, string encoding = "utf-8") where T : class
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("Method", "Post");
                httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }
                //将对象序列化成字典
                var dic = ModelHelper.convertToDictionary(t);

                HttpContent content = new FormUrlEncodedContent(dic);
                httpClient.PostAsync(url, content).ContinueWith((task) =>
                {
                    HttpResponseMessage response = task.Result;
                    // 确认响应成功，否则抛出异常  
                    response.EnsureSuccessStatusCode();
                    // 异步读取响应为字符串 
                    var contentType = response.Content.Headers.ContentType;
                    if (string.IsNullOrEmpty(contentType.CharSet))
                    {
                        contentType.CharSet = encoding;
                    }
                    response.Content.ReadAsStringAsync().ContinueWith(
                        (readTask) =>
                        {
                            var ret = readTask.Result;
                            //响应委托任务
                            httpDelegate(ret);
                        });
                });
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// GET异步请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpDelegate">委托</param>
        /// <param name="t"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        public static void getAsync<T>(string url, httpCallBackDelegate httpDelegate, T t = null, Dictionary<string, string> header = null, string encoding = "utf-8") where T : class
        {
            try
            {
                //添加必要的头
                httpClient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                if (header != null)
                {
                    foreach (var x in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(x.Key, x.Value);
                    }
                }
                //链接参数
                if (t != null)
                {
                    if (url.Contains("?"))
                    {
                        url += $"&{ModelHelper.convertToUrlParameter(t)}";
                    }
                    else
                    {
                        url += $"?{ModelHelper.convertToUrlParameter(t)}";
                    }
                }
                //请求
                httpClient.GetAsync(url).ContinueWith((task) =>
                {
                    HttpResponseMessage response = task.Result;
                    // 确认响应成功，否则抛出异常  
                    response.EnsureSuccessStatusCode();
                    // 异步读取响应为字符串  
                    response.Content.ReadAsStringAsync().ContinueWith(
                        (readTask) =>
                        {
                            var ret = readTask.Result;
                            //响应委托任务
                            httpDelegate(ret);
                        });
                });

            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
