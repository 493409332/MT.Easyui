/*
Author      : 张智
Date        : 2011-3-22
Description : 对 System.IComparable 的扩展
*/


using System;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 System.IComparable 的扩展
    /// </summary>
    public static class IComparableExtension
    {
        /// <summary>
        /// 可比较对象是否在指定的范围内
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">要比较的对象</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static bool InRange<T>(this T o, T minValue, T maxValue) where T : IComparable<T>
        {
            return o.CompareTo(minValue) >= 0 && o.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        /// 如果对象不在指定的范围内则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">要比较的对象</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T IfNotInRange<T>(this T o, T minValue, T maxValue, T defaultValue) where T : IComparable<T>
        {
            if (!InRange(o, minValue, maxValue))
                return defaultValue;

            return o;
        }
    }
}
