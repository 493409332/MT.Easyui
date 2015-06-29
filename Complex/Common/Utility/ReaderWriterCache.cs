/*
Author      : 张智
Date        : 2011-3-7
Description : 读写缓存
*/

using System.Collections.Generic;
using System.Threading;

namespace Complex.Common.Utility
{

    /// <summary>
    /// 读写缓存
    /// 内部维护一个读写锁 实现一种如果存在则返回原来的数据否则就创建并且将其缓存的机制
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class ReaderWriterCache<TKey, TValue>
    {
        /// <summary>
        /// 创建数据选择性的将其缓存
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="cacheResult">是否缓存数据</param>
        /// <returns></returns>
        public delegate T CreatorOrCache<T>(out bool cacheResult);
        /// <summary>
        /// 创建数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns></returns>
        public delegate T Creator<T>();
        private readonly Dictionary<TKey, TValue> _cache;
        private readonly ReaderWriterLockSlim _rwLockSlim = new ReaderWriterLockSlim();

        protected ReaderWriterCache()
            : this(null)
        {
        }

        protected ReaderWriterCache(IEqualityComparer<TKey> comparer)
        {
            _cache = new Dictionary<TKey, TValue>(comparer);
        }

        protected Dictionary<TKey, TValue> Cache
        {
            get
            {
                return _cache;
            }
        }


        /// <summary>
        /// 如果存在则返回原来的数据否则就创建并且将其缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        protected TValue FetchOrCreateItem(TKey key, CreatorOrCache<TValue> creator)
        {

            _rwLockSlim.EnterReadLock();
            try
            {
                TValue existingEntry;
                if (_cache.TryGetValue(key, out existingEntry))
                {
                    return existingEntry;
                }
            }
            finally
            {
                _rwLockSlim.ExitReadLock();
            }

            bool cache;
            TValue newEntry = creator(out cache);
            //如果需要缓存
            if (cache)
            {
                _rwLockSlim.EnterWriteLock();
                try
                {
                    TValue existingEntry;
                    if (_cache.TryGetValue(key, out existingEntry))
                    {
                        return existingEntry;
                    }

                    _cache[key] = newEntry;

                }
                finally
                {
                    _rwLockSlim.ExitWriteLock();
                }
            }
            return newEntry;
        }

        /// <summary>
        /// 如果存在则返回原来的数据否则就创建并且将其缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        protected TValue FetchOrCreateItem(TKey key, Creator<TValue> creator)
        {
            return FetchOrCreateItem(key, (out bool b) =>
            {
                b = true;
                return creator();
            });
        }
    }
}
