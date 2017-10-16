using System;
using Microsoft.Extensions.Caching.Memory;

namespace WeChat.Standard.Common
{
    internal class CacheHelper
    {
        private static readonly IMemoryCache _cache;
        static CacheHelper()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        /// <summary>
        /// 设置缓存 time秒后自动删除
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="content">内容</param>
        /// <param name="time">秒</param>
        public static void add(string key, object content, int time=7200)
        {
            _cache.Set(key, content, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(time)));
        }
        /// <summary>
        /// 根据KEY获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T get<T>(string key) where T : class
        {
            try
            {
                return _cache.Get(key) as T;
            }
            catch//缓存可能不存在
            {
                return null;
            }
        }
    }
}
