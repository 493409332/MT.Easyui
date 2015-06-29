/*
Author      : 张智
Date        : 2011-9-23
Description : 基于物理文件的文件存储器
*/


using System;
using System.IO;

namespace Complex.Common.Utility
{

    /// <summary>
    /// 基于物理文件的文件存储器
    /// </summary>
    public class PhysicsFileStorage : IFileStorage
    {

        public PhysicsFileStorage(int bufferSize = 4096)
        {
            this.BufferSize = bufferSize;
        }

        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public int BufferSize
        {
            get;
            private set;
        }

        public void Save(System.IO.Stream stream, string fileName)
        {
            if (!Path.IsPathRooted(fileName))
                throw new ArgumentException("文件名必须是绝对路径", "fileName");

            FileUtil.InsurePath(fileName);

            using (var fs = File.Open(fileName, FileMode.Create))
            {
                var bufferSize = this.BufferSize;
                var buffer = new byte[bufferSize];
                var len = 0;

                while ((len = stream.Read(buffer, 0, bufferSize)) > 0)
                {
                    fs.Write(buffer, 0, len);
                }

                fs.Flush();
            }
        }

        public string MakeFileName(string ext)
        {
            return "\\" + FileUtil.GetRandomFileName(ext);
        }
    }
}
