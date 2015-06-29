using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 System.Enum的扩展
    /// </summary>
    public static class EnumExtension
    {
        class EnumCache : ReaderWriterCache<Type, Dictionary<long, EnumItem>>
        {
            public Dictionary<long, EnumItem> GetEnumMap(Type t, Creator<Dictionary<long, EnumItem>> cr)
            {
                return FetchOrCreateItem(t, cr);
            }
        }

        #region 私有成员
        static readonly EnumCache _instance = new EnumCache();

        static Dictionary<long, EnumItem> fetchOrCreateEnumMap(Type t)
        {
            return _instance.GetEnumMap(t, () => createEnumMap(t));
        }
        static Dictionary<long, EnumItem> createEnumMap(Type t)
        {
            Dictionary<long, EnumItem> _map = new Dictionary<long, EnumItem>();
            FieldInfo[] fields = t.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo f in fields)
            {
                long v = Convert.ToInt64(f.GetValue(null));
                DescriptionAttribute[] ds = (DescriptionAttribute[])f.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (ds.Length > 0)
                {
                    _map[v] = new EnumItem { Value = v, Description = ds[0].Description };
                }
            }
            return _map;
        }


        #endregion

        /// <summary>
        /// 返回该枚举类型的所有枚举项成员以及描述 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static List<EnumItem> GetTypeItemList<EnumType>()
        {
            Type t = typeof(EnumType);
            return fetchOrCreateEnumMap(t).Values.ToList();
        }

        /// <summary>
        ///返回单枚举值的描述信息
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum v)
        {
            Type t = v.GetType();
            var map = fetchOrCreateEnumMap(t);
            EnumItem item;

            if (map.TryGetValue(Convert.ToInt64(v), out item))
            {
                return item.Description;
            }

            return string.Empty;
        }

        /// <summary>
        /// 返回按位组合枚举值 所构成的每一个值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<long> GetValues(this Enum values)
        {
            Type t = values.GetType();
            long lv = Convert.ToInt64(values);
            Dictionary<long, EnumItem> _map = fetchOrCreateEnumMap(t);
            var items = new List<long>();
            foreach (var item in _map)
            {
                var v = item.Key;
                if ((v & lv) == v)
                {
                    items.Add(v);
                }
            }

            return items;
        }


        /// <summary>
        ///  返回将按位组合枚举值的每一个值描述连接起来的字符串
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GetDescriptions(this Enum v)
        {
            Type t = v.GetType();
            Dictionary<long, EnumItem> _map = fetchOrCreateEnumMap(t);
            long lv = Convert.ToInt64(v);
            StringBuilder sb = new StringBuilder();
            var emtor = _map.Where(i => (i.Key & lv) == i.Key).GetEnumerator();
            if (emtor.MoveNext())
            {
                sb.Append(emtor.Current.Value.Description);
            }
            while (emtor.MoveNext())
            {
                sb.AppendFormat(",{0}", emtor.Current.Value.Description);
            }
            return sb.ToString();
        }


    }
}
