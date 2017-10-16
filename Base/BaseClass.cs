using System;
using WeChat.Standard.Model.Self;
using WeChat.Standard.Model;
using WeChat.Standard.Common;

namespace WeChat.Standard.Base
{
    internal class BaseClass
    {
        /// <summary>
        /// 获取微信的AccessToken
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string getAccessToken(WeChatInfo info)
        {
            var key = $"WeChatApplicationWithAccessToken_{info.appid}";
            var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={info.appid}&secret={info.appsecret}";
            //获取缓存中的值
            var tokenCache = CacheHelper.get<string>(key);
            //如果没有,则重新远程获取并加入缓存
            if (string.IsNullOrEmpty(tokenCache))
            {
                //远程拉取字符串
                var str = getFormWechatService(url);
                //反序列化
                AccessToken token = JsonHelper.jsonToModel<AccessToken>(str);
                tokenCache = token.access_token;

                //存入缓存,绝对过期时间
                CacheHelper.add(key, token.access_token, token.expires_in);
            }
            return tokenCache;
        }
        /// <summary>
        /// 获取微信的JsApiTicket
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string getJsApi_Ticket(WeChatInfo info)
        {
            var key = $"WeChatApplicationWithJsApiTicket_{info.appid}";
            var access_token = getAccessToken(info);
            var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={access_token}&type=jsapi";
            //获取缓存中的值
            var apiCache = CacheHelper.get<string>(key);
            //如果没有,则重新远程获取并加入缓存
            if (string.IsNullOrEmpty(apiCache))
            {
                //远程拉取字符串
                var str = getFormWechatService(url);
                //反序列化
                JsApiTicket ticket = JsonHelper.jsonToModel<JsApiTicket>(str);
                apiCache = ticket.ticket;

                //存入缓存,绝对过期时间
                CacheHelper.add(key, apiCache, ticket.expires_in);
            }
            return apiCache;
        }

        public static string getFormWechatService(string url)
        {
            try
            {
                //远程拉取字符串
                var data = HttpHelper.get<string>(url);
                var err = JsonHelper.jsonToModel<ErrorMessage>(data);
                if (err.errcode == 0)
                {
                    return data;
                }
                else
                {
                    throw new Exception($"拉取数据错误:{data}");
                }

            }
            catch(Exception ex)
            {
                throw new Exception($"getFormWechatService:{ex.Message}");
            }
        }
        public static string postFormWechatService(string str, string url)
        {
            try
            {
                //远程拉取字符串
                var data = HttpHelper.post(url,str);
                var err = JsonHelper.jsonToModel<ErrorMessage>(data);
                if (err.errcode == 0)
                {
                    return data;
                }
                else
                {
                    throw new Exception($"拉取数据错误:{data}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"postFormWechatService:{ex.Message}");
            }
        }
    }
}
