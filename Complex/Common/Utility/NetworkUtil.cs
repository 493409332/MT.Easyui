/*
Author      : 张智
Date        : 2011-3-7
Description : 提供网络应用常用功能
*/

using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Complex.Common.Utility.Extensions;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 提供网络应用常用功能
    /// </summary>
    public static class NetworkUtil
    {
        /// <summary>
        /// 以GB2312编码方式获取远程WEB服务器路径的HTML
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            return GetHtml(url, Encoding.GetEncoding("GB2312"));
        }

        /// <summary>
        /// 获取远程WEB服务器路径的HTML允许自定义HTTP头和响应字符编码
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="headers">HTTP头</param>
        /// <param name="Encoding">字符编码</param>
        /// <returns></returns>
        public static string GetHtml(string url, NameValueCollection headers, Encoding encoding)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (url.Length == 0)
                throw new ArgumentException("url不能为空");

            if (encoding == null)
                encoding = Encoding.GetEncoding("GB2312");

            HttpWebRequest objRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            objRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";

            if (headers != null && headers.Count > 0)
                objRequest.Headers.Add(headers);

            WebResponse objResponse = objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream(), encoding))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 获取远程WEB服务器路径的HTML允许自定义响应字符编码
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="Encoding">字符编码</param>
        /// <returns></returns>
        public static string GetHtml(string url, Encoding encoding)
        {
            return GetHtml(url, null, encoding);
        }

        /// <summary>
        /// 将IPV4地址字符串转换为相应的有符号的64位整数
        /// </summary>
        /// <param name="ipString">IP字符串</param>
        /// <returns></returns>
        public static long ToLongFromIPv4(string ipString)
        {
            if (string.IsNullOrEmpty(ipString) ||
                !FormatValidate.IsIPv4(ipString))
                return -1;

            string[] bitArray = ipString.SplitRemoveEmpty(".");
            long val = 0;
            val = Convert.ToInt64(bitArray[0]) << 24;
            val += Convert.ToInt64(bitArray[1]) << 16;
            val += Convert.ToInt64(bitArray[2]) << 8;
            val += Convert.ToInt64(bitArray[3]);
            return val;
        }
    }
}
