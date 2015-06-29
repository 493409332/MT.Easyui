/*
Author      : 沈进坤
Date        : 2011-4-20
Description : 对 DateTime 的扩展
Modify:
 *  2011-8-25   张智  修改方法    WeekOfYear(this DateTime date)
 *                   新增方法     GetfirstDayOfThisYear(this DateTime date)
 *                              GetFirstDayOfThisQuarter(this DateTime date)
 *                              GetFirstDayOfThisMonth(this DateTime date)
 *                              GetFirstDayOfThisWeek(this DateTime date)
*/
using System;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 DateTime 的扩展
    /// </summary>
    public static class DateTimeExtension
    {

        /// <summary>
        /// 获得本年的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetfirstDayOfThisYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }


        /// <summary>
        /// 获得本月的最后一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfThisMonth(this DateTime date)
        {
            return Convert.ToDateTime(date.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
        }



        /// <summary>
        /// 时间转long类型 类似20130101235959
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToLongValue(this DateTime date)
        {
            long v = date.Year * 10000000000 + date.Month * 100000000 + date.Day * 1000000 + date.Hour * 10000 + date.Minute * 100 + date.Second;
            return v;
        }

        /// <summary>
        /// 时间转long类型短日期 类似20130101
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToShortValue(this DateTime date)
        {
            long v = date.Year * 10000 + date.Month * 100 + date.Day;
            return v;
        }

        /// <summary>
        /// 日期转long类型 类似20130101
        /// </summary>
        /// <param name="date"></param>
        /// <returns>返回日期</returns>
        public static long ToLongValueByDate(this DateTime date)
        {
            long v = date.Year * 10000 + date.Month * 100 + date.Day;
            return v;
        }


        /// <summary>
        /// 获得本季度的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfThisQuarter(this DateTime date)
        {
            return new DateTime(date.Year, ((date.Month - 1) / 3) * 3 + 1, 1);
        }

        /// <summary>
        /// 获得本月的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfThisMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获得本周的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfThisWeek(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return date.Date;

            return date.Date.AddDays(-(int)date.DayOfWeek);
        }

        /// <summary>
        /// 获取时间 是一年中的第几个星期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime date)
        {
            var firstDay = new DateTime(date.Year, 1, 1);
            var days = (date.Date - firstDay).Days + 1 + (int)firstDay.DayOfWeek;
            return (int)Math.Ceiling(days / 7.0);
        }

    }
}
