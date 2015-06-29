using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Complex.Common.Utility
{
    public static class MD5Hashing
    {
        private static readonly MD5 md5 = MD5.Create();
        //private MD5Hashing()
        //{
        //}
        /**/

        /// <summary>
        /// 将字符串加密
        /// </summary>
        /// <param name="sourceString">需要加密的字符串</param>
        /// <returns>MD5加密后字符串</returns>
        public static string HashString(string sourceString)
        {
            return HashString("gb2312", sourceString);
        }

        /**/

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="codeName">编码类型</param>
        /// <param name="sourceString">需要加密的字符串</param>
        /// <returns>MD5加密后字符串</returns>
        public static string HashString(string codeName, string sourceString)
        {
            byte[] source = md5.ComputeHash(Encoding.GetEncoding(codeName).GetBytes(sourceString));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 与asp兼容的md5加密
        /// </summary>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static string Md5(string strPassword)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return
                BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(strPassword))).Replace("-", "").
                    ToLower();
        }

        //但是当加密字符串含有中文或者其它双字节字符时,这种算法的结果与目前网上流行的ASP写的MD5算法的结果却不一致,这主要是由于目前网上流行的ASP写的MD5加密算法,存在一个缺陷,它使用了mid函数，取出的是“字符”，而正确的做法应该是取出字节,因此当加密字符串有双字节字符时,结果会与标准的MD5算法不一致.。但是由于在ASP向ASP.net的系统进行升级的过程中，已经向数据库内写入了大量以前ASP算法加密的密码，为了使新系统能够与原来的系统完全兼容，因此只有在.net 环境下实现与原来ASP算法完全一致的MD5算法。
        //----------------------------------------------------


        /// <summary>
        /// 目前ASP.NET通用的MD5加密类
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Md5Code(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）  
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32) //32位加密  
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return "00000000000000000000000000000000";
        }
    }
}