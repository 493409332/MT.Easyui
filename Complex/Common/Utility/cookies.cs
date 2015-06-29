using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Complex.Common.Utility.Extensions;
using Utility;

namespace Complex.Common.Utility
{
    public static class cookies
    {
        #region Cookie操作
        /// <summary>
        /// 将对象序列化后以Base64字符串写入cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        public static void WriteCookie(string name, object value)
        {
            Byte[] data = value.BinarySerialize();
            WriteCookie(name, Convert.ToBase64String(data));
        }

        /// <summary>
        /// 将对象序列化后以Base64字符串写入cookie 并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, string aesKey)
        {
            Byte[] data = value.BinarySerialize();
            data = SecurityUtil.AESEncrypt(data, Encoding.UTF8.GetBytes(aesKey));
            WriteCookie(name, Convert.ToBase64String(data));
        }


        /// <summary>
        /// 将对象序列化后以Base64字符串写入cookie 并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="domain">cookie的域名</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, string aesKey, string domain)
        {
            Byte[] data = value.BinarySerialize();
            data = SecurityUtil.AESEncrypt(data, Encoding.UTF8.GetBytes(aesKey));
            string strValue = Convert.ToBase64String(data);

            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = strValue;

            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;

            WriteCookie(cookie);
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie  并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expiresMinute">过期时间(分钟)</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, int expiresMinute, string aesKey)
        {
            WriteCookie(name, value, DateTime.Now.AddMinutes(expiresMinute), aesKey);
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie 并且设置过期分钟
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expiresMinute">过期时间(分钟)</param>
        public static void WriteCookie(string name, object value, int expiresMinute)
        {
            WriteCookie(name, value, DateTime.Now.AddMinutes(expiresMinute));
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie 并且设置过期时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expires">过期时间</param>
        public static void WriteCookie(string name, object value, DateTime expires)
        {
            Byte[] data = value.BinarySerialize();
            WriteCookie(name, Convert.ToBase64String(data), expires);
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie 并且设置过期时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expires">过期时间</param>
        ///  <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, DateTime expires, string aesKey)
        {
            Byte[] data = value.BinarySerialize();
            data = SecurityUtil.AESEncrypt(data, Encoding.UTF8.GetBytes(aesKey));
            WriteCookie(name, Convert.ToBase64String(data), expires);
        }

        /// <summary>
        /// 向HTTP响应写入cookie值 并且对cookie值进行AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, string aesKey)
        {
            value = value.AESEncrypt(aesKey);
            WriteCookie(name, value);
        }

        /// <summary>
        /// 将字符串写入cookie 并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="domain">cookie的域名</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, string aesKey, string domain)
        {
            value = value.AESEncrypt(aesKey);
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;

            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;

            WriteCookie(cookie);
        }

        /// <summary>
        /// 向HTTP响应写入cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public static void WriteCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            WriteCookie(cookie);
        }

        /// <summary>
        ///  向HTTP响应写入cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void WriteCookie(HttpCookie cookie)
        {
            if (cookie == null) return;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期分钟
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresMinute">过期时间(分钟)</param>
        public static void WriteCookie(string name, string value, int expiresMinute)
        {
            WriteCookie(name, value, DateTime.Now.AddMinutes(expiresMinute));
        }

        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期时间和对cookie值进行AES加密
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expiresMinute"></param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, int expiresMinute, string aesKey)
        {
            value = value.AESEncrypt(aesKey);
            WriteCookie(name, value, expiresMinute);
        }

        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期时间 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        public static void WriteCookie(string name, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = expires;
            WriteCookie(cookie);
        }


        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期时间和对cookie值进行AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, DateTime expires, string aesKey)
        {
            value = value.AESEncrypt(aesKey);
            WriteCookie(name, value, expires);
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveCookie(string name)
        {
            HttpCookie hk = new HttpCookie(name);
            hk.Value = string.Empty;
            hk.Expires = DateTime.Now.AddYears(-1);
            WriteCookie(hk);
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveCookie(string name, string domain)
        {
            HttpCookie hk = new HttpCookie(name);
            hk.Domain = domain;
            hk.Value = string.Empty;
            hk.Expires = DateTime.Now.AddDays(-1);
            WriteCookie(hk);
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="cookie">cookie</param>
        public static void RemoveCookie(HttpCookie cookie)
        {
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读取请求中的Cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>cookie值</returns>
        public static string ReadCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                return cookie.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 读取请求中的Base64 Cookie值并将其反序列化为指定的类型， 如果不存在该值则返回默认值
        /// </summary>
        /// <typeparam name="T">将要反序列化的目标类型</typeparam>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static T ReadCookie<T>(string name)
        {
            string cookieValue = ReadCookie(name);
            if (string.IsNullOrEmpty(cookieValue))
                throw new NullReferenceException("请确保是否包含该cookie值");

            byte[] objBytes;
            try
            {
                objBytes = Convert.FromBase64String(cookieValue);
            }
            catch
            {
                throw new FormatException("请确保cookie值是否符合base64格式");
            }
            using (MemoryStream ms = new MemoryStream(objBytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }
        }

        /// <summary>
        /// 读取请求中的Cookie值并将其用128位 AES解密 将解密后的值反序列化为指定的类型
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="aesKey">进行解密的128位密钥</param>
        /// <returns></returns>
        public static T ReadCookie<T>(string name, string aesKey)
        {
            string cookieValue = ReadCookie(name);

            if (string.IsNullOrEmpty(cookieValue))
                throw new NullReferenceException("请确保是否包含该cookie值");

            byte[] objBytes;
            try
            {
                objBytes = Convert.FromBase64String(cookieValue);
            }
            catch
            {
                throw new FormatException("请确保cookie值是否符合base64格式");
            }

            try
            {
                objBytes = SecurityUtil.AESDecrypt(objBytes, Encoding.UTF8.GetBytes(aesKey));
            }
            catch
            {
                throw new CryptographicException("解密失败");
            }

            using (MemoryStream ms = new MemoryStream(objBytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }

        }

        /// <summary>
        /// 读取请求中的Cookie值并将其用128位 AES解密 如果不存在该值或者解密失败都将返回String.Empty
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="aesKey">进行解密的128位密钥</param>
        /// <returns>如果不存在该值或者解密失败都将返回String.Empty</returns>
        public static string ReadCookie(string name, string aesKey)
        {
            string value = ReadCookie(name);

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            try
            {
                value = value.AESDecrypt(aesKey);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return value;
        }

        #endregion
    }
}
