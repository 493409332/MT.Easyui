using System;

namespace Complex.Common.Cache
{
    /// <summary>
    /// 缓存策略接口
    /// </summary>
    internal interface ICacheStrategy
    {
        /// <summary>
        /// 添加一个对象到缓存 
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        void Add(string key, object value, DateTime timeout);

        void AddSecondCache(string key, object value, int seconds);
        /// <summary>
        /// 添加一个对象到缓存并且与指定的文件组建立依赖
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <param name="files">文件组的依赖</param>
        ///<param name="cacheKeys">其他缓存key的依赖</param>
        /// <returns></returns>
        void AddWithDepend(string key, object value, DateTime timeout, string[] files, string[] cacheKeys);
        /// <summary>
        /// 获得某个缓存值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        object Get(string key);
        /// <summary>
        /// 删除某个缓存值 并且将其返回
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void Remove(string key);

        System.Collections.IDictionaryEnumerator GetAll();
    }
}
