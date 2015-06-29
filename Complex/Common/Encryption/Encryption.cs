using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Complex.Common.Encryption
{
    public class Encryption
    {
        public Encryption(/*string key*/)
       {
           //_encryptionKey = key;
       }

        /// <summary>
        /// 加密key
        /// </summary>
        private readonly string _encryptionKey = "!~^$_#%!~^$_#%!~^$_#%";
        /// <summary>
        /// 产生盐值key
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 可逆加密字符串
        /// </summary>
        /// <param name="plainText">被加密字符串</param>
        /// <param name="encryptionPrivateKey">加密盐值，如果为空，使用默认</param>
        /// <returns></returns>
        public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _encryptionKey;

            var tDeSalg = new TripleDESCryptoServiceProvider();
            tDeSalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0,16));
            tDeSalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDeSalg.Key, tDeSalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText">被加密字符串</param>
        /// <param name="encryptionPrivateKey">加密盐值，如果为空，使用默认</param>
        /// <returns></returns>
        public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _encryptionKey;

            var tDeSalg = new TripleDESCryptoServiceProvider();
            tDeSalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDeSalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDeSalg.Key, tDeSalg.IV);
        }
        #region Utilities

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion
    }
}
