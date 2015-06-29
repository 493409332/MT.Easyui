/*
Author      : 张智
Date        : 2011-3-16
Description : 默认的验证图片生成器
*/


using System.Drawing;
using System.Drawing.Drawing2D;
using Utility.ValidateImage;

namespace Complex.Common.Utility.ValidateImage
{
    /// <summary>
    /// 默认的验证图片生成器
    /// </summary>
    public class DefaultValidateImageGenerator : IValidateImageGenerator
    {
        readonly static Color[] _backgroundSurroundColors = new Color[] { Color.Red, Color.FromArgb(250, 220, 2) };
        readonly static Color[] _textPathSurroundColors = new Color[] { Color.Yellow, Color.Red, Color.Tan, Color.Yellow, Color.FromArgb(241, 155, 6) };

        #region IValidateImageGenerator 成员

        /// <summary>
        /// 产生验证图片
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        public Image GenerateImage(string text, Font font)
        {
            using (StringFormat textFormat = StringFormat.GenericTypographic)
            {
                textFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                Size textBlockSize;
                //创建一个初始图片 在上面测量要生成的文本块的大小
                using (Bitmap bmp = new Bitmap(1, 1))
                {
                    using (Graphics gh = Graphics.FromImage(bmp))
                    {
                        //测量文本的所占矩形大小,以便让其自动计算图片大小
                        textBlockSize = gh.MeasureString(text, font).ToSize();
                    }
                }
                //建立最终输出图片
                Image textImage;
                int w = textBlockSize.Width, h = textBlockSize.Height;
                textImage = new Bitmap(w, h);
                try
                {
                    //建立新的画布
                    using (Graphics grapTextImage = Graphics.FromImage(textImage))
                    {
                        Point[] ps = new Point[] {
                                                    new Point(0,0),
                                                    new Point(w,0),
                                                    new Point(w,h),
                                                    new Point(0,h) };
                        //建立一个路径渐变画笔
                        using (PathGradientBrush bBackground = new System.Drawing.Drawing2D.PathGradientBrush(ps))
                        {
                            bBackground.CenterColor = Color.Yellow;
                            //设置渐变过渡色彩
                            bBackground.SurroundColors = _backgroundSurroundColors;
                            //填充颜色到画布
                            grapTextImage.FillRectangle(bBackground, 0, 0, w, h);
                        }
                        //建立路径
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            //添加文本路径
                            path.AddString(text, font.FontFamily, 1, font.Size, new PointF(0.0F, 0.0F), textFormat);
                            //建立一个文字路径渐变
                            using (PathGradientBrush textGradient = new PathGradientBrush(path))
                            {
                                textGradient.CenterColor = Color.Yellow;
                                textGradient.SurroundColors = _textPathSurroundColors;
                                grapTextImage.FillRectangle(textGradient, 0, 0, w, h);

                                using (LinearGradientBrush lb = new LinearGradientBrush(new Point(0, 0), new Point(w, h), Color.Yellow, Color.FromArgb(239, 239, 0)))
                                {
                                    grapTextImage.DrawPath(new Pen(lb), path);
                                }

                            }
                        }
                        grapTextImage.Save();
                    }
                }
                catch
                {
                    textImage.Dispose();
                    throw;
                }

                return textImage;
            }

        }

        #endregion
    }
}
