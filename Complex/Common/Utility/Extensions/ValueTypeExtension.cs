using System;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 值类型 的扩展
    /// </summary>
    public static class ValueTypeExtension
    {

        /// <summary>
        /// 将可空值类型安全的转换到 System.String 如果可空值类型为null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultString">当可空值类型为null时返回的默认字符串</param>
        /// <returns></returns>
        public static string SafeToString<T>(this Nullable<T> value, string defaultString) where T : struct
        {
            if (!value.HasValue)
                return defaultString;

            return value.Value.ToString();
        }

        /// <summary>
        /// 将可空值类型安全的转换到 System.String 如果可空值类型为null 则返回String.Empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SafeToString<T>(this Nullable<T> value) where T : struct
        {
            return SafeToString<T>(value, string.Empty);
        }
    }
}
