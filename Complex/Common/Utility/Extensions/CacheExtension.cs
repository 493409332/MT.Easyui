using System;

namespace Complex.Common.Utility.Extensions
{
   
    using System.Web.Caching;
    /// <summary>
    /// 对 System.Web.Caching.Cache的扩展
    /// </summary>
    public static class CacheExtension
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        static public object Insert(this Cache cache, string key, object value, DateTime time, string dependencieFile, CacheItemPriority priority)
        {
            cache.Insert(key, value, dependencieFile == null ? null : new CacheDependency(dependencieFile), time, Cache.NoSlidingExpiration, priority, null);
            return value;
        }
        /// <summary>
        /// 添加缓存 使用CacheItemPriority.Default等级
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="time">过期时间</param>
        /// <param name="dependencieFile">依赖文件</param>
        static public object Insert(this Cache cache, string key, object value, DateTime time, string dependencieFile)
        {
            cache.Insert(key, value, time, dependencieFile, CacheItemPriority.Default);
            return value;
        }
        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="time">过期时间</param>
        /// <returns></returns>
        static public object Get(this Cache cache, string key, Func<object> createData, DateTime time)
        {
            return cache.Get(key) ?? Insert(cache, key, createData(), time, null);
        }
        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="time">过期时间</param>
        /// <returns></returns>
        static public T Get<T>(this Cache cache, string key, Func<T> createData, DateTime time)
        {
            return cache.Get<T>(key, createData, time, null);
        }

        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="time">过期时间</param>
        /// <param name="dependencieFile">依赖文件</param>
        /// <returns></returns>
        static public T Get<T>(this Cache cache, string key, Func<T> createData, DateTime time, string dependencieFile)
        {
            return (T)(cache.Get(key) ?? Insert(cache, key, createData(), time, dependencieFile));
        }
    }
}
