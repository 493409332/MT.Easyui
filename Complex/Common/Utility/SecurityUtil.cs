/*
Author      : 张智
Date        : 2011-3-18
Description : 提供常用的安全功能
*/

using System;
using System.Security.Cryptography;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 提供常用的安全功能
    /// </summary>
    public static class SecurityUtil
    {
        #region 私有成员
        /// <summary>
        /// 默认的64位加密初始化向量
        /// </summary>
        static readonly Byte[] _defaultRgbIV64 = { 0x12, 0x65, 0xa3, 0xcd, 0x90, 0x4f, 0x53, 0xfc };

        /// <summary>
        /// 默认的128位加密初始化向量
        /// </summary>
        static readonly Byte[] _defaultRgbIV128 = { 0x12, 0x65, 0xa3, 0xcd, 0x90, 0x4f, 0x53, 0xfc, 0x1f, 0xa5, 0xa1, 0xcd, 0x10, 0xef, 0x5b, 0xac };
        #endregion

        #region 加密解密

        #region MD5

        /// <summary>
        ///  对数据进行MD5加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <returns></returns>
        public static byte[] MD5Encrypt(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            using (MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }

        #endregion

        #region DES

        /// <summary>
        /// 对数据进行DES加密 
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="rgbIV">64位的初始化向量</param>
        /// <param name="key">进行加密的64位密钥</param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] data, byte[] rgbIV, byte[] key)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");


            if (key.Length != 8)
                throw new CryptographicException("密钥是64位");

            if (rgbIV.Length != 8)
                throw new CryptographicException("初始化向量是64位");


            using (DES des = DES.Create())
            {
                ICryptoTransform cryp = des.CreateEncryptor(key, rgbIV);
                return cryp.TransformFinalBlock(data, 0, data.Length);
            }

        }

        /// <summary>
        /// 对数据进行DES加密 
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="key">进行加密的64位密钥</param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] data, byte[] key)
        {
            return DESEncrypt(data, _defaultRgbIV64, key);
        }


        /// <summary>
        /// 对已进行DES加密的数据进行解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="rgbIV">64位的初始化向量</param>
        /// <param name="key">进行解密的64位密钥</param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] data, byte[] rgbIV, byte[] key)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");

            if (key.Length != 8)
                throw new CryptographicException("密钥是64位");

            if (rgbIV.Length != 8)
                throw new CryptographicException("初始化向量是64位");


            using (DES des = DES.Create())
            {
                ICryptoTransform cryp = des.CreateDecryptor(key, rgbIV);
                return cryp.TransformFinalBlock(data, 0, data.Length);
            }

        }

        /// <summary>
        ///  对已使用固定向量进行DES加密的数据进行解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="key">进行解密的64位密钥</param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] data, byte[] key)
        {
            return DESDecrypt(data, _defaultRgbIV64, key);
        }

        #endregion

        #region AES

        /// <summary>
        /// 对数据使用指定向量来进行128位 AES对称加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="rgbIV">128位的初始化向量</param>
        /// <param name="key">进行加密的128位密钥</param>
        /// <returns></returns>
        public static byte[] AESEncrypt(byte[] data, byte[] rgbIV, byte[] key)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");


            if (key.Length != 16)
                throw new CryptographicException("密钥应该是128位");

            if (rgbIV.Length != 16)
                throw new CryptographicException("初始化向量应该是128位");


            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.IV = rgbIV;
                rm.Key = key;

                ICryptoTransform cryp = rm.CreateEncryptor();
                return cryp.TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        ///   对数据使用指定向量来进行128位 AES对称加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="key">进行加密的128位密钥</param>
        /// <returns></returns>
        public static byte[] AESEncrypt(byte[] data, byte[] key)
        {
            return AESEncrypt(data, _defaultRgbIV128, key);
        }

        /// <summary>
        /// 对已进行128位 AES加密的Base64字符串进行解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="rgbIV">128位的初始化向量</param>
        /// <param name="key">进行解密的128位密钥</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(byte[] data, byte[] rgbIV, byte[] key)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");


            if (key.Length != 16)
                throw new CryptographicException("密钥应该是128位");

            if (rgbIV.Length != 16)
                throw new CryptographicException("初始化向量应该是128位");


            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.IV = rgbIV;
                rm.Key = key;

                ICryptoTransform cryp = rm.CreateDecryptor();
                return cryp.TransformFinalBlock(data, 0, data.Length);

            }

        }

        /// <summary>
        ///  对已使用固定向量进行128位 AES加密的数据进行解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="key">进行解密的128位密钥</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(byte[] data, byte[] key)
        {
            return AESDecrypt(data, _defaultRgbIV128, key);
        }
        #endregion

        #endregion

        /// <summary>
        /// 删除字符串中的可能引起不安全因素的SQL字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveUnsafeSqlString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return FormatValidate._unsafeSqlStringRule.Replace(str, string.Empty);
        }
    }
}
