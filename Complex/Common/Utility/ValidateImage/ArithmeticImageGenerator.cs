using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Complex.Common.Utility.Extensions;
using Utility.ValidateImage;

namespace Complex.Common.Utility.ValidateImage
{
    /// <summary>
    /// 算术题 验证码生成器
    /// </summary>
    public class ArithmeticImageGenerator : IValidateImageGenerator
    {

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public System.Drawing.Image GenerateImage(string text, System.Drawing.Font font)
        {
            Random randx = new Random();
            int Padding = 4;
            int fSize = 14;
            int fWidth = fSize + Padding;
            MatchCollection match = Regex.Matches(text, "[\u4e00-\u9fa5]", RegexOptions.IgnoreCase);

            int imageWidth = (int)((text.Length + match.Count) * fWidth) + 4 + Padding * 2;

            int imageHeight = fSize * 2 + Padding;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            Color foreground_color = Color.FromArgb(randx.Next(10, 100), randx.Next(10, 100), randx.Next(10, 100));
            Pen pen = new Pen(foreground_color, 0);
            int c = 50;

            for (int i = 0; i < c; i++)
            {
                int x = randx.Next(image.Width);
                int y = randx.Next(image.Height);

                g.DrawRectangle(pen, x, y, 1, 1);
            }

            int left = 0, top = 0, top1 = 1, top2 = 1;
            int n1 = (imageHeight - 14 - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            //Font f = new System.Drawing.Font("宋体", fSize, System.Drawing.FontStyle.Bold);

            
            Brush b = new System.Drawing.SolidBrush(foreground_color);
            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }
                left = j * fWidth;
                string txt = text.Substring(i, 1);
                if (txt.IsChinese())
                {
                    j += 2;
                }
                else
                {
                    j++;
                }
                if (left == 0)
                    left = 10;
                g.DrawString(txt, font, b, left, top);
            }

            int m = randx.Next(3, 10); 
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();
            //image = TwistImage(image, true, m, 4);
            //Graphics g2 = Graphics.FromImage(image);
            ////g2.Clear(Color.White);
            //g2.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            //g2.Dispose();
            return image;
        }

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
    }
}
