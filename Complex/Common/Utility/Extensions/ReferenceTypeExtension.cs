using System;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 引用类型 的扩展
    /// </summary>
    public static class ReferenceTypeExtension
    {
        /// <summary>
        /// 当前对象是否为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        /// <summary>
        /// 当前对象为非null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }

        /// <summary>
        /// 如果对象为null 则执行相应的初始化调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static T IfNull<T>(this T obj, Func<T> ret) where T : class
        {
            if (obj == null)
                return ret();

            return obj;
        }

        /// <summary>
        /// 如果对象为null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T IfNull<T>(this T obj, T defaultValue) where T : class
        {
            if (obj == null)
                return defaultValue;

            return obj;
        }

        /// <summary>
        /// 如果对象为null 则执行相应的动作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void IfNull<T>(this T obj, Action action) where T : class
        {
            if (obj == null)
                action();
        }

        public static V IfNull<T, V>(this T obj, Func<T, V> func, V defaultValue) where T : class
        {
            if (obj == null)
                return func(obj);

            return defaultValue;
        }
        //

        /// <summary>
        /// 如果对象为不null 则执行相应的初始化调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static T IfNotNull<T>(this T obj, Func<T> ret) where T : class
        {
            if (obj != null)
                return ret();

            return obj;
        }

        /// <summary>
        /// 如果对象为不null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T IfNotNull<T>(this T obj, T defaultValue) where T : class
        {
            if (obj != null)
                return defaultValue;

            return obj;
        }


        //

        /// <summary>
        ///  如果对象不为null 则执行相应的动作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void IfNotNull<T>(this T obj, Action action) where T : class
        {
            if (obj != null)
                action();
        }

        /// <summary>
        /// 如果对象不为null 则执行相应的动作返回V类型值 否则返回defaultValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V">要返回的类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="func"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static V IfNotNull<T, V>(this T obj, Func<T, V> func, V defaultValue) where T : class
        {
            if (obj != null)
                return func(obj);

            return defaultValue;
        }

        /// <summary>
        /// 将对象安全的转换到 System.String 如果对象为null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultString">当对象为null时返回的默认字符串</param>
        /// <returns></returns>
        public static string SafeToString<T>(this T obj, string defaultString) where T : class
        {
            if (obj == null)
                return defaultString;

            return obj.ToString();
        }

        /// <summary>
        /// 将对象安全的转换到 System.String 如果对象为null 则返回String.Empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SafeToString<T>(this T obj) where T : class
        {
            return SafeToString<T>(obj, string.Empty);
        }

    }
}
