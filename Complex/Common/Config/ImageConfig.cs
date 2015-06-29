using System;
using System.IO;

namespace Complex.Common.Config
{
    public sealed class ImageConfig
    {
        public static string ProductImgPath = System.Configuration.ConfigurationManager.AppSettings["ProductImgPath"];
        public static string ArticleImgPath = System.Configuration.ConfigurationManager.AppSettings["ArticleImgPath"];
        public static string GameImgPath = System.Configuration.ConfigurationManager.AppSettings["GameImgPath"];

        public static string ProductImgDomain = System.Configuration.ConfigurationManager.AppSettings["ProductImgDomain"];
        public static string ProductImgDescnDomain = System.Configuration.ConfigurationManager.AppSettings["ProductImgDescnDomain"];
        public static string ArticleImgDomain = System.Configuration.ConfigurationManager.AppSettings["ArticleImgDomain"];
        public static string GameImgDomain = System.Configuration.ConfigurationManager.AppSettings["GameImgDomain"];


//#if DEBUG
//        public static string ProductImgPath = @"D:\soft\website\web\webimg\";
//        public static string ArticleImgPath = @"D:\soft\website\web\webimg\";
//        public static string GameImgPath = @"D:\soft\website\web\webimg\";

//        public static string ProductImgDomain = "http://www.5378.com/webimg/ProductImg/";
//        public static string ProductImgDescnDomain = "http://www.5378.com/webimg/ProductImg/Descn/";
//        public static string ArticleImgDomain = "http://www.5378.com/webimg/ProductImg/";
//        public static string GameImgDomain = "http://www.5378.com/webimg/ProductImg/";


//#else
//        public static string ProductImgPath = @"D:\soft\website\web\webimg\";
//        public static string ArticleImgPath = @"D:\soft\website\web\webimg\";
//        public static string GameImgPath = @"D:\soft\website\web\webimg\";

//        public static string ProductImgDomain = "http://www.5378.com/webimg/ProductImg/";
//        public static string ProductImgDescnDomain = "http://www.5378.com/webimg/ProductImg/Descn/";
//        public static string ArticleImgDomain = "http://www.5378.com/webimg/ProductImg/";
//        public static string GameImgDomain = "http://www.5378.com/webimg/ProductImg/";
//#endif

        public static int SmallImgWidth = 100;
        public static int SmallImgHeight = 100;


        public static int MediumImgWidth = 200;
        public static int MediumImgHeight = 200;

        public static string RadomFileName(string ext)
        {
            var random = new Random(DateTime.Now.Millisecond);
            return DateTime.Now.ToString("yyyyMMddhhmmss") + random.Next(10000) + "." + ext;
        }

        public static string GetFileExt(string fullPath)
        {
            return fullPath == "" ? "" : fullPath.Substring(fullPath.LastIndexOf('.') + 1).ToLower();
        }

        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        }


    }
}