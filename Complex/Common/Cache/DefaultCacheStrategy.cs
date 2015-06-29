
using System;
using System.Web;
using System.Web.Caching;

namespace Complex.Common.Cache
{
    /// <summary>
    /// 默认缓存策略
    /// </summary>
    internal class DefaultCacheStrategy : ICacheStrategy
    {
        private static volatile System.Web.Caching.Cache _webCache = HttpRuntime.Cache;
        private const CacheItemPriority cacheItemPriority = CacheItemPriority.Default;
        #region ICacheStrategy 成员

        public void Add(string key, object value, DateTime timeout)
        {
            _webCache.Insert(key, value, null, timeout, System.Web.Caching.Cache.NoSlidingExpiration, cacheItemPriority, null);
        }
        public void AddSecondCache(string key, object value, int seconds)
        {
            _webCache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(seconds));
        }
        public void AddWithDepend(string key, object value, DateTime timeout, string[] files, string[] cacheKeys)
        {
            _webCache.Insert(key, value, new CacheDependency(files, cacheKeys, DateTime.Now), timeout, System.Web.Caching.Cache.NoSlidingExpiration, cacheItemPriority, null);
        }
        public object Get(string key)
        {
            return _webCache[key];
        }
        public void Remove(string key)
        { 
             _webCache.Remove(key);
        }
        public System.Collections.IDictionaryEnumerator GetAll()
        {
           return _webCache.GetEnumerator();
        }
        #endregion
    }
}
