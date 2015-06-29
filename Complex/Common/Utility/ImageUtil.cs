/*
Author      : 张智
Date        : 2011-3-14
Description : 提供常用的图片处理功能
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Text;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 提供常用的图片处理功能
    /// </summary>
    public static class ImageUtil
    {
        /// <summary>
        /// 表示相当于图片的对齐位置
        /// </summary>
        public enum Location
        {
            /// <summary>
            /// 左上角
            /// </summary>
            LeftTop,
            /// <summary>
            /// 左下角
            /// </summary>
            LeftBottom,
            /// <summary>
            /// 右上角
            /// </summary>
            RightTop,
            /// <summary>
            /// 右下角
            /// </summary>
            RightBottom,
            /// <summary>
            /// 正中
            /// </summary>
            Center
        }

        #region 私有成员

        /// <summary>
        /// 内置编码器映射表
        /// </summary>
        internal static readonly Dictionary<string, ImageCodecInfo> EncoderMap = new Dictionary<string, ImageCodecInfo>(StringComparer.OrdinalIgnoreCase);
        internal static readonly Dictionary<Guid, ImageCodecInfo> EncoderGuidMap = new Dictionary<Guid, ImageCodecInfo>();
        static ImageUtil()
        {

            ImageCodecInfo[] codecInfos = ImageCodecInfo.GetImageEncoders();//取得内置的编码器
            //将内置编码器和文件扩展名进行映射
            foreach (ImageCodecInfo ici in codecInfos)
            {
                EncoderGuidMap[ici.FormatID] = ici;
                string[] exts = ici.FilenameExtension.Split(';');
                foreach (var e in exts)
                {
                    EncoderMap[e.Substring(1)] = ici;
                }
            }
        }

        static void saveImage(Image image, string fileName, long quality)
        {
            string ext = Path.GetExtension(fileName);
            FileUtil.InsurePath(fileName);
            using (var fs = File.Create(fileName))
            {
                SaveImage(image, fs, quality, ext);
                fs.Flush();
            }
        }

        /// <summary>
        /// 用指定的文件扩展名对应的图像编码器将Image对象保存到指定的流中
        /// </summary>
        /// <param name="image"></param>
        /// <param name="stream"></param>
        /// <param name="quality">图像质量 1 - 100 </param>
        /// <param name="ext"></param>
        public static void SaveImage(Image image, Stream stream, long quality, string ext)
        {

            ImageCodecInfo ici;
            if (!EncoderMap.TryGetValue(ext, out ici))
            {
                throw new ArgumentException("fileName", "不存在指定的编码器");
            }

            using (EncoderParameters eps = new EncoderParameters(1))
            {
                eps.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                image.Save(stream, ici, eps);
            }
        }


        /// <summary>
        /// 位置转换
        /// </summary>
        /// <param name="osize"></param>
        /// <param name="msize"></param>
        /// <param name="loc"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        static void conversionLocation(Size osize, Size msize, Location loc, int offsetX, int offsetY, out int x, out int y)
        {
            switch (loc)
            {
                case Location.LeftTop:
                    x = offsetX;
                    y = offsetY;
                    break;
                case Location.LeftBottom:
                    x = offsetX;
                    y = osize.Height - msize.Height - offsetY;
                    break;
                case Location.RightTop:
                    x = osize.Width - msize.Width - offsetX;
                    y = offsetY;
                    break;
                case Location.RightBottom:
                    x = osize.Width - msize.Width - offsetX;
                    y = osize.Height - msize.Height - offsetY;
                    break;
                default:
                    x = (osize.Width - msize.Width) / 2 + offsetX;
                    y = (osize.Height - msize.Height) / 2 + offsetY;
                    break;
            }
        }

        /// <summary>
        /// 计算将指定大小等比缩放到指定范围后的新大小
        /// </summary>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <param name="currentSize">当前的大小</param>
        /// <returns>新大小</returns>
        static Size accountZoomSize(int maxWidth, int maxHeight, Size currentSize)
        {
            if (currentSize.Width > maxWidth || currentSize.Height > maxHeight)
            {

                double widthProportion = maxWidth / (double)currentSize.Width;  //宽度比例
                double heightProprotion = maxHeight / (double)currentSize.Height;   //高度比例

                //如果宽度比例小于高度比例那么以宽度比例缩放
                if (widthProportion < heightProprotion)
                {
                    return new Size((int)(currentSize.Width * widthProportion),
                                    (int)(currentSize.Height * widthProportion));
                }
                else
                {
                    return new Size((int)(currentSize.Width * heightProprotion),
                                    (int)(currentSize.Height * heightProprotion));
                }
            }

            return currentSize;
        }

        /// <summary>
        /// 测量为本大小
        /// </summary>
        /// <param name="image"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        static Size measureString(Size grapSize, string text, Font font, StringFormat stringFormat)
        {
            using (Image tImage = new Bitmap(grapSize.Width, grapSize.Height))
            {
                using (Graphics imageGrap = Graphics.FromImage(tImage))//图片画布
                {
                    return imageGrap.MeasureString(text, font, grapSize.Width, stringFormat).ToSize();
                }
            }
        }


        static Image drawTextBlock(Size grapSize, string text, Color color, Font font)
        {
            using (StringFormat textFormat = StringFormat.GenericTypographic)
            {
                textFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                Size textBlockSize = measureString(grapSize, text, font, textFormat);
                Bitmap textBmp = new Bitmap(textBlockSize.Width, textBlockSize.Height);
                try
                {
                    using (Graphics textGrap = Graphics.FromImage(textBmp))
                    {
                        Brush brush = new SolidBrush(color); //文字画笔
                        textGrap.CompositingQuality = CompositingQuality.HighSpeed;
                        textGrap.SmoothingMode = SmoothingMode.HighSpeed;
                        textGrap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        textGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
                        textGrap.DrawString(text, font, brush, new PointF(0, 0), textFormat);
                        textGrap.Save();
                        return textBmp;
                    }
                }
                catch
                {
                    textBmp.Dispose();
                    throw;
                }
            }

        }
        #endregion

        /// <summary>
        /// 将16进制颜色代码转换为Colo对象
        /// </summary>
        /// <param name="base16str">16进制颜色代码 如:#FFFFF</param>
        /// <returns></returns>
        public static Color Base16ToColor(string base16str)
        {
            if (base16str == null)
                throw new ArgumentNullException("base16str");

            if (base16str.Length != 0 && base16str[0] == '#')
            {
                base16str = base16str.Substring(1);
            }

            base16str = base16str.PadRight(6, '0');

            return Color.FromArgb(
                Convert.ToInt32(base16str.Substring(0, 2), 16),
                Convert.ToInt32(base16str.Substring(2, 2), 16),
                Convert.ToInt32(base16str.Substring(4, 2), 16)
            );
        }

        #region 文本绘制

        /// <summary>
        /// 在指定的Image对象上绘制文字 并且可以设置其格式大小位置等
        /// </summary>
        /// <param name="image">被绘制的Image对象</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="color">文本的颜色</param>
        /// <param name="font">文本的字体</param>
        /// <param name="location">文本的对齐位置</param>
        /// <param name="offsetX">文本相对于对齐位置的X偏移量</param>
        /// <param name="offsetY">文本相对于对齐位置的Y偏移量</param>
        /// <param name="opacity">文本的不透明性 1 - 100</param>
        public static void DrawText(Image image, string text, Color color, Font font, Location location, int offsetX, int offsetY, int opacity)
        {
            using (Image textBlock = drawTextBlock(image.Size, text, color, font))
            {
                int x, y;
                conversionLocation(image.Size, textBlock.Size, location, offsetX, offsetY, out x, out y);
                DrawImage(image, textBlock, x, y, opacity);
            }
        }


        /// <summary>
        /// 在指定的Image对象的指定位置绘制文字
        /// </summary>
        /// <param name="image">被绘制的Image对象</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="color">文本的颜色</param>
        /// <param name="font">文本的字体</param>
        /// <param name="x">文本的x位置</param>
        /// <param name="y">文本的y位置</param>
        /// <param name="opacity">文本的不透明性 1 - 100</param>
        public static void DrawText(Image image, string text, Color color, Font font, int x, int y, int opacity)
        {
            using (Image textBlock = drawTextBlock(image.Size, text, color, font))
            {
                DrawImage(image, textBlock, x, y, opacity);
            }
        }

        /// <summary>
        ///在指定的图片上绘制文字 并且可以设置其格式大小位置等
        /// </summary>
        /// <param name="imageFileName">要被绘制的图像的文件名</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="newFileName">要保存的文件名</param>
        /// <param name="color">文本的颜色</param>
        /// <param name="font">文本的字体</param>
        /// <param name="location">文本的对齐位置</param>
        /// <param name="offsetX">文本相对于对齐位置的X偏移量</param>
        /// <param name="offsetY">文本相对于对齐位置的Y偏移量</param>
        /// <param name="opacity">文本的不透明性 1 - 100</param>
        /// <param name="quality">图像保存的质量 1-100之间的整数</param>
        public static void DrawText(string imageFileName, string text, string newFileName, Color color, Font font, Location location, int offsetX, int offsetY, int opacity, long quality)
        {
            Image oImage = null;
            using (Image originalImage = Image.FromFile(imageFileName))
            {
                oImage = new Bitmap(originalImage, originalImage.Size);
            }

            try
            {
                DrawText(oImage, text, color, font, location, offsetX, offsetY, opacity);
                saveImage(oImage, newFileName, quality);
            }
            finally
            {
                oImage.Dispose();
            }
        }

        /// <summary>
        /// 在指定的图片上的右下角位置绘制文字 并且可以设置其格式大小位置等
        /// </summary>
        /// <param name="imageFileName">要被绘制的图像的文件名</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="newFileName">要保存的文件名</param>
        /// <param name="color">文本的颜色</param>
        /// <param name="font">文本的字体</param>
        /// <param name="opacity">文本的不透明性 1 - 100</param>
        public static void DrawTextOnRightBottom(string imageFileName, string text, string newFileName, Color color, Font font, int opacity)
        {
            DrawText(imageFileName, text, newFileName, color, font, Location.RightBottom, 10, 10, opacity, 100L);
        }
        #endregion

        #region 图片绘制

        /// <summary>
        /// 将Image对象绘制到另外一个Image对象上
        /// </summary>
        /// <param name="originalImage">原Image对象</param>
        /// <param name="drawImage">将要被绘制的Image对象</param>
        /// <param name="x">绘制到原Image对象的X坐标位置</param>
        /// <param name="y">绘制到原Image对象的Y坐标位置</param>
        /// <param name="opacity">将要被绘制的Image对象的透明度</param>
        public static void DrawImage(Image originalImage, Image drawImage, int x, int y, int opacity)
        {
            using (Graphics oimageGrap = Graphics.FromImage(originalImage))//图片画布
            {
                oimageGrap.CompositingQuality = CompositingQuality.HighSpeed;
                oimageGrap.SmoothingMode = SmoothingMode.HighSpeed;
                oimageGrap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                oimageGrap.TextRenderingHint = TextRenderingHint.AntiAlias;

                Size drawImageSize = drawImage.Size;

                if (opacity < 100)
                {
                    #region 设置不透明性时

                    float[][] singleArrayArray2 = new float[5][];
                    float[] singleArray1 = new float[5];
                    singleArray1[0] = 1f;
                    singleArrayArray2[0] = singleArray1;
                    float[] singleArray2 = new float[5];
                    singleArray2[1] = 1f;
                    singleArrayArray2[1] = singleArray2;
                    float[] singleArray3 = new float[5];
                    singleArray3[2] = 1f;
                    singleArrayArray2[2] = singleArray3;
                    float[] singleArray4 = new float[5];
                    singleArray4[3] = ((float)opacity) / 100f;
                    singleArrayArray2[3] = singleArray4;
                    float[] singleArray5 = new float[5];
                    singleArray5[4] = 1f;
                    singleArrayArray2[4] = singleArray5;
                    float[][] singleArrayArray1 = singleArrayArray2;
                    ColorMatrix matrix1 = new ColorMatrix(singleArrayArray1);
                    using (ImageAttributes attributes1 = new ImageAttributes())
                    {
                        attributes1.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        oimageGrap.DrawImage(drawImage, new Rectangle(x, y, drawImageSize.Width, drawImageSize.Height), 0, 0, drawImageSize.Width, drawImageSize.Height, GraphicsUnit.Pixel, attributes1);

                    }
                    #endregion
                }
                else
                {
                    oimageGrap.DrawImage(drawImage, new Rectangle(x, y, drawImageSize.Width, drawImageSize.Height), 0, 0, drawImageSize.Width, drawImageSize.Height, GraphicsUnit.Pixel);
                }
                oimageGrap.Save();
            }
        }

        /// <summary>
        /// 将Image对象绘制到另外一个Image对象上 并且可以通过偏移量来调整位置 设置不透明性来调节显示效果
        /// </summary>
        /// <param name="originalImage">原Image对象</param>
        /// <param name="drawImage">将要被绘制的Image对象</param>
        /// <param name="location">被绘制的图片的对齐位置</param>
        /// <param name="offsetX">被绘制的图片相对于对齐位置的X偏移量</param>
        /// <param name="offsetY">被绘制的图片相对于对齐位置的Y偏移量</param>
        /// <param name="opacity">被绘制的图片的不透明性 1 - 100</param>
        public static void DrawImage(Image originalImage, Image drawImage, Location location, int offsetX, int offsetY, int opacity)
        {
            Size drawImageSize = drawImage.Size;
            int x, y;
            //根据位置设置和偏移量计算出合适的绝对位置
            conversionLocation(originalImage.Size, drawImageSize, location, offsetX, offsetY, out x, out y);
            DrawImage(originalImage, drawImage, x, y, opacity);
        }

        /// <summary>
        /// 将一个图片绘制到另外一个图片上 并且可以通过偏移量来调整位置 设置不透明性来调节显示效果
        /// </summary>
        /// <param name="originalImageFileName">原图文件名</param>
        /// <param name="drawImageFileName">要绘制的文件名</param>
        /// <param name="newFileName">要保存的文件名</param>
        /// <param name="location">被绘制的图片的对齐位置</param>
        /// <param name="offsetX">被绘制的图片相对于对齐位置的X偏移量</param>
        /// <param name="offsetY">被绘制的图片相对于对齐位置的Y偏移量</param>
        /// <param name="opacity">被绘制的图片的不透明性 1 - 100</param>
        /// <param name="quality">图像保存的质量 1-100之间的整数</param>
        public static void DrawImage(string originalImageFileName, string drawImageFileName, string newFileName, Location location, int offsetX, int offsetY, int opacity, long quality)
        {
            Image oImage = null;
            using (Image originalImage = Image.FromFile(originalImageFileName))
            {
                oImage = new Bitmap(originalImage, originalImage.Size);
            }

            try
            {
                using (Image drawImage = Image.FromFile(drawImageFileName))
                {
                    DrawImage(oImage, drawImage, location, offsetX, offsetY, opacity);
                }

                saveImage(oImage, newFileName, quality);
            }
            finally
            {
                oImage.Dispose();
            }
        }


        /// <summary>
        ///  将一个图片绘制到另外一个图片的右下角 x和y10像素偏移量的位置
        /// </summary>
        /// <param name="originalImageFileName">原图文件名</param>
        /// <param name="drawImageFileName">要绘制的文件名</param>
        /// <param name="newFileName">要保存的文件名</param>
        /// <param name="opacity">被绘制的图片的不透明性 1 - 100</param>
        public static void DrawImageOnRightBottom(string originalImageFileName, string drawImageFileName, string newFileName, int opacity)
        {
            DrawImage(originalImageFileName, drawImageFileName, newFileName, Location.RightBottom, 10, 10, opacity, 100L);
        }

        #endregion

        #region 图片缩放

        /// <summary>
        /// 将Image对象等比缩放的指定的范围内 并保持该范围内的等比最大尺寸
        /// </summary>
        /// <param name="originalImage">原始图像</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <returns>一个新的Image对象</returns>
        public static Image MakeThumbnailImage(Image originalImage, int maxWidth, int maxHeight)
        {
            Size _newSize = accountZoomSize(maxWidth, maxHeight, originalImage.Size);
            return originalImage.GetThumbnailImage(_newSize.Width, _newSize.Height, null, IntPtr.Zero);
            /**
            Image _newImage = new Bitmap(_newSize.Width, _newSize.Height);
            using (Graphics orbmpgs = Graphics.FromImage(_newImage))
            {
                orbmpgs.CompositingQuality = CompositingQuality.HighSpeed;
                orbmpgs.SmoothingMode = SmoothingMode.HighSpeed;
                orbmpgs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                orbmpgs.DrawImage(originalImage, new Rectangle(0, 0, _newSize.Width, _newSize.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel);
            }
            return _newImage;
             * */
        }

        /// <summary>
        /// 将指定图片等比缩放的指定的范围内 并保持该范围内的等比最大尺寸
        /// </summary>
        /// <param name="fileName">要缩放的图片文件名</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <returns>一个Image对象</returns>
        public static Image MakeThumbnailImage(string fileName, int maxWidth, int maxHeight)
        {
            using (Image _originalImage = Image.FromFile(fileName))
            {
                return MakeThumbnailImage(_originalImage, maxWidth, maxHeight);
            }
        }

        /// <summary>
        /// 将指定图片等比缩放的指定的范围内 并将其保持为指定文件
        /// </summary>
        /// <param name="fileName">要缩放的图片文件名</param>
        /// <param name="newFileName">要保存的新文件名</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <param name="quality">图像保存的质量 1-100之间的整数</param>
        public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight, long quality)
        {
            using (Image _originalImage = Image.FromFile(fileName))
            {
                using (Image _newImage = MakeThumbnailImage(_originalImage, maxWidth, maxHeight))
                {
                    saveImage(_newImage, newFileName, quality);
                }
            }
        }

        /// <summary>
        /// 将指定图片等比缩放的指定的范围内 并将其保持为指定文件
        /// </summary>
        /// <param name="fileName">要缩放的图片文件名</param>
        /// <param name="newFileName">要保存的新文件名</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight)
        {
            MakeThumbnailImage(fileName, newFileName, maxWidth, maxHeight, 100L);
        }

        #endregion
    }
}
