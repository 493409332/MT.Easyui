using System.IO;
using System.Text;

namespace Complex.Common.Config
{
    public sealed class FileConfig
    {
        /// <summary>
        ///返回文件内容.
        /// </summary>
        /// <param name="path">文件件物理路径</param>
        /// <returns>文件内容</returns>
        public static string LoadFileContent(string path)
        {
            if (!File.Exists(path)) return "";
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (fs == null) throw new IOException("Unable to open the file: " + path);
            var sr = new StreamReader(fs, Encoding.UTF8);
            string res = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return res;
        }
    }
}
