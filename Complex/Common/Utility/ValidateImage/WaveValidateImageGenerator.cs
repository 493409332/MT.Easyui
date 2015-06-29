/*
Author      : 沈进坤
Date        : 2011-5-6
Description : 验证码扭曲生成器
*/
using System;
using System.Drawing;
using Complex.Common.Utility.Extensions;
using Complex.Common.Utility.ValidateImage;
using Utility;
using System.Text.RegularExpressions;

namespace Utility.ValidateImage
{
    /// <summary>
    /// 验证码扭曲生成器
    /// </summary>
    public class WaveValidateImageGenerator : IValidateImageGenerator
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public System.Drawing.Image GenerateImage(string text, System.Drawing.Font font)
        {
            int Padding = 4;
            int fSize = 14;
            int fWidth = fSize + Padding;
            MatchCollection match = Regex.Matches(text, "[\u4e00-\u9fa5]", RegexOptions.IgnoreCase);

            int imageWidth = (int)((text.Length+match.Count) * fWidth) + 4 + Padding * 2;

            int imageHeight = fSize * 2 + Padding;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);

            int left = 0, top = 0, top1 = 1, top2 = 1;
            int n1 = (imageHeight - 14 - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            //Font f = new System.Drawing.Font("宋体", fSize, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.Black);
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
            g.Dispose();

            Color foreground_color;
            image = VerifyCodeWave.WaveDistortion(image, out foreground_color);

            Graphics g2 = Graphics.FromImage(image);
            Random rand = new Random();


            Pen pen = new Pen(foreground_color, 1);

            for (int i = 0; i < 10; i++)
            {
                int x0 = rand.Next(image.Width);
                int y0 = rand.Next(image.Height);
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g2.DrawLine(pen, (float)x0, (float)y0, (float)x, (float)y);
            }

            g2.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g2.Dispose();
            return image;
        }
    }
}
