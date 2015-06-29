using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 IEnumerable 的扩展
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 对IEnumerable<T>的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="action">要执行的操作</param>
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var item in ie)
            {
                action(item);
            }
        }

        /// <summary>
        /// 对IEnumerable的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="action">要执行的操作</param>
        public static void ForEach<T>(this IEnumerable ie, Action<T> action)
        {
            foreach (var item in ie)
            {
                action((T)item);
            }
        }

        /// <summary>
        ///  在指定  IEnumerable`T 的每个元素之间串联指定的分隔符 System.String，从而产生单个串联的字符串。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="toText">将T转换为文本</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> ie, Func<T, string> toText, string separator)
        {
            if (ie == null)
                return null;

            StringBuilder sb = new StringBuilder();
            var enumtor = ie.GetEnumerator();
            if (enumtor.MoveNext())
            {
                sb.Append(toText(enumtor.Current));
                while (enumtor.MoveNext())
                {
                    sb.Append(separator);
                    sb.Append(toText(enumtor.Current));
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// 在指定  IEnumerable`T 的每个元素之间串联指定的分隔符 System.String，从而产生单个串联的字符串。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> ie, string separator)
        {
            if (ie == null)
                return null;

            return ie.Join((T v) => v.ToString(), separator);
        }
        /// <summary>
        /// 去重
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
