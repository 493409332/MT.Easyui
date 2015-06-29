using System;
using System.IO;
using System.Web;

namespace Complex.Common.Cache
{
    #region 窃取响应数据的过滤器
    /// <summary>
    /// 窃取响应数据的过滤器
    /// </summary>
    internal class FilchResponFilter : Stream
    {
        private Stream _orStream;
        private MemoryStream _memoryStream;
        public FilchResponFilter(Stream stream)
        {
            this._orStream = stream;
        }
        /// <summary>
        /// 是否窃取数据
        /// </summary>
        public bool Filch
        {
            get;
            set;
        }
        public override bool CanRead
        {
            get { return this._orStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this._orStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this._orStream.CanWrite; }
        }

        public override void Flush()
        {
            this._orStream.Flush();
        }

        public override long Length
        {
            get { return this._orStream.Length; }
        }

        public override long Position
        {
            get
            {
                return this._orStream.Position;
            }
            set
            {
                this._orStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this._orStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this._orStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this._orStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.Filch)
            {
                if (this._memoryStream == null)
                    this._memoryStream = new MemoryStream();

                this._memoryStream.Write(buffer, offset, count);
            }
            else
            {
                this._orStream.Write(buffer, offset, count);
            }
        }

        public byte[] FilchBytes()
        {
            if (this._memoryStream == null)
                return null;

            var bytes = this._memoryStream.ToArray();
            this._memoryStream.SetLength(0);
            return bytes;
        }
    }
    #endregion

    public static class MyCacheExtension
    {
        /// <summary>
        /// 将action内产生的所有输出数据进行缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存key</param>
        /// <param name="action">产生输出的方法</param>
        /// <param name="timeout">超时时间</param>
        public static void CachePartial(this MyCache cache, string key, Action action, DateTime timeout)
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException("不存在的http上下文");

            var response = context.Response;
            byte[] cacheBuffer = cache.Get<byte[]>(key, () =>
            {
                //清空掉缓冲区的数据
                var fr = new FilchResponFilter(response.Filter);
                fr.Filch = true;

                response.Flush();
                response.Filter = fr;   //通过过滤器准备窃取action产生的输出数据
                action();
                response.Flush();   //再次刷新缓冲区以便将action所有产生数据窃取
                var bs = fr.FilchBytes();
                fr.Filch = false;

                return bs;

            }, timeout);

            if (cacheBuffer != null && cacheBuffer.Length > 0)
            {
                response.OutputStream.Write(cacheBuffer, 0, cacheBuffer.Length);
            }

        }
        /// <summary>
        /// 将action内产生的所有输出数据进行缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存key</param>
        /// <param name="action">产生输出的方法</param>
        /// <param name="timeout">超时分钟数</param>
        public static void CachePartial(this MyCache cache, string key, Action action, int timeout)
        {
            CachePartial(cache, key, action, DateTime.Now.AddMinutes(timeout));
        }

        /// <summary>
        /// 将action内产生的所有输出数据进行缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存key</param>
        /// <param name="action">产生输出的方法</param>
        public static void CachePartial(this MyCache cache, string key, Action action)
        {
            CachePartial(cache, key, action, DateTime.MaxValue);
        }
    }
}
