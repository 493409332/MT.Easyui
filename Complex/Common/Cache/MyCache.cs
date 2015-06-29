using System;

namespace Complex.Common.Cache
{
    /// <summary>
    /// 写入缓存或者读取缓存
    /// </summary>
    public class MyCache
    {
        private MyCache()
        {
        } 
        static readonly ICacheStrategy _strategy = new DefaultCacheStrategy();
        static readonly MyCache _instance = new MyCache();
        /// <summary>
        /// 缓存实例
        /// </summary>
        static public MyCache Instance
        {
            
            get
            {
                return _instance;
            }
        }
        public void RemoveStartsWith(string key)
        {
            var quer = _strategy.GetAll();
            while ( quer.MoveNext() )
            {

                if ( quer.Key.ToString().StartsWith(key) )
                {
                    Remove(quer.Key.ToString());
                }
               
            } ; 

          
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <returns></returns>
        public void Add(string key, object value, int timeout)
        {
            Add(key, value, DateTime.Now.AddMinutes(timeout));
        }
        public void AddSecond(string key, object value, int seconds)
        {
            AddSecondCache(key, value, seconds);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public void Add(string key, object value, DateTime timeout)
        {
            if (value == null)
            {
             //   Debug.Assert(false);
                return;
            }
            _strategy.Add(key, value, timeout);
        }
        public void AddSecondCache(string key, object value,int seconds)
        {
            if (value == null)
            {
                //   Debug.Assert(false);
                return;
            }
            _strategy.AddSecondCache(key, value, seconds);
        }
        /// <summary>
        /// 添加缓存并且与文件列表建立依赖关系 并且永久存放
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public void AddWithDependFiles(string key, object value, string[] files)
        {
            AddWithDepend(key, value, DateTime.MaxValue, files, null);
        }
        /// <summary>
        /// 添加缓存并且与文件列表建立依赖关系
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public void AddWithDependFiles(string key, object value, int timeout, string[] files)
        {
            AddWithDepend(key, value, DateTime.Now.AddMinutes(timeout), files, null);
        }

        /// <summary>
        /// 添加缓存并且与文件列表建立依赖关系
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时时间</param>
        /// <param name="files">文件组</param>
        /// <returns></returns>
        public void AddWithDependFiles(string key, object value, DateTime timeout, string[] files)
        {
            AddWithDepend(key, value, timeout, files, null);
        }

        /// <summary>
        /// 添加缓存并且与文件组和缓存组建立依赖关系
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时时间</param>
        /// <param name="files">文件组</param>
        /// <param name="cacheKeys">缓存组</param>
        /// <returns></returns>
        public void AddWithDepend(string key, object value, DateTime timeout, string[] files, string[] cacheKeys)
        {
            if (value == null)
            {
               // Debug.Assert(false);
                return;
            }
            _strategy.AddWithDepend(key, value, timeout, files, cacheKeys);
        }

        /// <summary>
        /// 返回缓存值
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _strategy.Get(key);
        }

        /// <summary>
        /// 返回缓存值
        /// </summary>
        /// <typeparam name="T">将缓存的值转换为T</typeparam>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <returns></returns>
        public T Get<T>(string key) where T:class
        {
            var quer = Get(key);
            if ( quer is T )
            {
                return (T) Get(key);
            }
            else
            {
                return default(T);
            }
           
        }
        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存再返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <returns></returns>
        public object Get(string key, Func<object> createData)
        {
            object value = Get(key);
            if (value == null)
            {
                value = createData();
                Add(key, value);
            }
            return value;
        }
        public object GetSecondCache(string key, Func<object> createData,int seconds)
        {
            object value = Get(key);
            if (value == null)
            {
                value = createData();
                AddSecond(key, value, seconds);
            }
            return value;
        }
        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存再返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <returns></returns>
        public object Get(string key, OrCache createData)
        {
            object value = Get(key);
            if (value == null)
            {
                bool need;
                value = createData(out need);
                if (need)
                {
                    Add(key, value);
                }
            }
            return value;
        }

        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <returns></returns>
        public T Get<T>(string key, Func<object> createData)
        {
            return (T)Get(key, createData);
        }

        public T GetSecondCache<T>(string key, Func<object> createData,int seconds)
        {
            return (T)GetSecondCache(key, createData, seconds);
        }

        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <returns></returns>
        public T Get<T>(string key, OrCache createData)
        {
            return (T)Get(key, createData);
        }

        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <returns></returns>
        public object Get(string key, Func<object> createData, int timeout)
        {
            return Get(key, createData, DateTime.Now.AddMinutes(timeout));
        }
        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <returns></returns>
        public object Get(string key, OrCache createData, int timeout)
        {
            return Get(key, createData, DateTime.Now.AddMinutes(timeout));
        }


        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <returns></returns>
        public T Get<T>(string key, Func<object> createData, int timeout)
        {
            return (T)Get(key, createData, timeout);
        }

        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <returns></returns>
        public T Get<T>(string key, OrCache createData, int timeout)
        {
            return (T)Get(key, createData, timeout);
        }


        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public object Get(string key, Func<object> createData, DateTime timeout)
        {
            object value = Get(key);
            if (value == null)
            {
                value = createData();
                Add(key, value, timeout);
            }
            return value;
        }

        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="path">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public object Get(string key, OrCache createData, DateTime timeout)
        {
            object value = Get(key);
            if (value == null)
            {
                bool need;
                value = createData(out need);
                if (need)
                {
                    Add(key, value, timeout);
                }
            }
            return value;
        }


        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public T Get<T>(string key, Func<object> createData, DateTime timeout)
        {
            return (T)Get(key, createData, timeout);
        }

        /// <summary>
        /// 返回缓存数据 如果数据不存在则新建数据并且将其缓存在返回
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="createData">当缓存中数据为空时 通过执行这个方法将数据加入到缓存并且返回</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public T Get<T>(string key, OrCache createData, DateTime timeout)
        {
            return (T)Get(key, createData, timeout);
        }

        /// <summary>
        /// 添加缓存并且与其他缓存key建立依赖关系 并且永久存放
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="keys">与之依赖关联的key列表</param>
        public void AddWithDependKeys(string key, object value, string[] keys)
        {
            AddWithDepend(key, value, DateTime.MaxValue, null, keys);
        }

        /// <summary>
        /// 添加缓存并且与其他缓存key建立依赖关系
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时分钟 1440分钟=24小时</param>
        /// <param name="keys">与之依赖关联的key列表</param>
        /// <returns></returns>
        public void AddWithDependKeys(string key, object value, int timeout, string[] keys)
        {
            AddWithDependKeys(key, value, DateTime.Now.AddMinutes(timeout), keys);
        }

        /// <summary>
        /// 添加缓存并且与其他缓存key建立依赖关系
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value"></param>
        /// <param name="timeout">超时时间</param>
        /// <param name="keys">与之依赖关联的key列表</param>
        /// <returns></returns>
        public void AddWithDependKeys(string key, object value, DateTime timeout, string[] keys)
        {
            AddWithDepend(key, value, timeout, null, keys);
        }

        /// <summary>
        ///返回并删除某缓存key的值
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <returns></returns>
        public void Remove(string key)
        {
            _strategy.Remove(key);
        }

        /// <summary>
        /// 添加对象到缓存 并且永久存放
        /// </summary>
        /// <param name="key">缓存key 如：/a/b/c/d</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public void Add(string key, object value)
        {
            this.Add(key, value, DateTime.MaxValue);
        }
    }
}
