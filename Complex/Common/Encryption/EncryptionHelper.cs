using Complex.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complex.Common.Encryption
{
    public sealed class EncryptionHelper
    {
        //private static readonly Encryption MyEencryption = new Encryption(FileConfig.LoadFileContent(@"C:\WINDOWS\system32\enc_key.txt"));
        private static readonly Encryption MyEencryption = new Encryption();

        public static string EncryptText(string inputText)
        {
            return MyEencryption.EncryptText(inputText);
        }

        public static string DecryptText(string inputText)
        {
            return MyEencryption.DecryptText(inputText);
        }

        public static string DecryptTextMainDbConnStr()
        {
            var ctx = FileConfig.LoadFileContent(@"C:\WINDOWS\system32\enc_ctx.txt");
            return MyEencryption.DecryptText(ctx);
        }
    }
}
