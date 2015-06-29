/*
Author      : 张智
Date        : 2011-3-7
Description : 提供文件常用功能
*/


using System;
using System.Text;
using System.IO;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 提供文件常用功能
    /// </summary>
    public static class FileUtil
    {
        #region 私有成员
        #endregion

        /// <summary>
        /// 获得一个按照当前系统时间生成的文件夹路径和随机文件名 如:2008\9\1\3\236548593.gif
        /// </summary>
        /// <param name="ext">文件的扩展名如:.gif</param>
        /// <returns>返回随机文件路径和名称</returns>
        public static string GetRandomFileName(string ext)
        {
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(ext) && ext[0] != '.')
            {
                ext = '.' + ext;
            }
            Random _randomGenerator = new Random();
            return string.Format(@"{0}\{1}\{2}\{3}\{4}{5}", now.Year, now.Month, now.Day, now.Hour, _randomGenerator.NextDouble().ToString().Substring(2), ext);
        }


        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 确保文件夹路径是存在的 如果不存在则创建
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static void InsurePath(string path)
        {
            path = Path.GetDirectoryName(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 生成快捷方式文件,并提供下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filename"></param>
        public static void CreateShortcut(string url, string filename)
        {
            StringBuilder shortcut = new StringBuilder();
            shortcut.AppendLine("[InternetShortcut]");
            shortcut.AppendLine("URL=" + url);
            shortcut.AppendLine("IDList=");
            shortcut.AppendLine("[{000214A0-0000-0000-C000-000000000046}]");
            shortcut.AppendLine("Prop3=19,2");

            byte[] Buffer = Encoding.UTF8.GetBytes(shortcut.ToString());
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.ContentType = "application/applefile";

            System.Web.HttpContext.Current.Response.BinaryWrite(Buffer);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Close();
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
