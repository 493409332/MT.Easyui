/*
Author      : 张智
Date        : 2011-3-7
Description : 提供常用格式验证
Modify:
 *  2011-31-23  沈进坤 增加方法 IsMobile(string str)
*/


using System;
using System.Text.RegularExpressions;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 提供常用格式验证
    /// </summary>
    public static class FormatValidate
    {
        #region 私有成员

        /// <summary>
        /// EMAIL 格式正则
        /// </summary>
        static readonly Regex _emailFormatRule = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);

        /// <summary>
        /// 中文字符 格式正则
        /// </summary>
        static readonly Regex _chineseFormatRule = new Regex("^[\u4e00-\u9fa5]*$", RegexOptions.Compiled);

        /// <summary>
        /// URL 格式正则
        /// </summary>
        static readonly Regex _urlFormatRule = new Regex(@"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$", RegexOptions.Compiled);

        /// <summary>
        /// IP地址 格式正则
        /// </summary>
        static readonly Regex _ipv4FormatRule = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$", RegexOptions.Compiled);

        /// <summary>
        /// BASE64 格式正则
        /// </summary>
        static readonly Regex _base64FromatRule = new Regex(@"[A-Za-z0-9\+\/\=]", RegexOptions.Compiled);

        /// <summary>
        /// 可能引起不安全因素的SQL字符
        /// </summary>
       internal static readonly Regex _unsafeSqlStringRule = new Regex(@"[-|;|,|\/|\[|\]|\}|\{|%|@|\*|!|\']", RegexOptions.Compiled);

        /// <summary>
        /// 是否为手机号
        /// </summary>
        static readonly Regex _mobile = new Regex(@"^(((1[3|4|5|8][0-9]{1}))+\d{8})", RegexOptions.Compiled);

        /// <summary>
        /// 数字 格式规则
        /// </summary>
        static readonly Regex _digital = new Regex(@"^[0-9]*$", RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// 返回字符串是否符合email格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return _emailFormatRule.IsMatch(str);
        }

        /// <summary>
        /// 返回字符串是否符合URL格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsURL(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return _urlFormatRule.IsMatch(str);
        }

        /// <summary>
        /// 返回字符串是否全是中文汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return _chineseFormatRule.IsMatch(str);
        }

        /// <summary>
        /// 返回字符串是否符合IPV4格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIPv4(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return _ipv4FormatRule.IsMatch(str);
        }

        /// <summary>
        /// 返回字符串是否符合BASE64字符格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return _base64FromatRule.IsMatch(str);
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns></returns>
        public static bool IsUnsafeSqlString(string str)
        {
            return _unsafeSqlStringRule.IsMatch(str);
        }

        /// <summary>
        /// 检查是否为手机号码
        /// </summary>
        /// <param name="str">待判断字符</param>
        /// <returns>true:为手机号;false:非手机号</returns>
        public static bool IsMobile(string str) {
            return _mobile.IsMatch(str);
        }

        /// <summary>
        /// 检查是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDigital(string str) {
            return _digital.IsMatch(str);
        }

    }
}
