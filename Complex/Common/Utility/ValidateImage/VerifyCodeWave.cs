/*
Author      : 沈进坤
Date        : 2011-5-6
Description : 验证码扭曲算法
*/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Complex.Common.Utility.ValidateImage
{
    /// <summary>
    /// 验证码扭曲算法
    /// </summary>
    public class VerifyCodeWave
    {
        #region  KCAPTCHA 波纹扭曲

        /// <summary>

        /// # KCAPTCHA PROJECT VERSION 1.2.6

        /// www.captcha.ru, www.kruglov.ru

        /// 波形扭曲 FROM KCAPTCHA

        /// </summary>

        /// <param name="srcBmp">待扭曲的图像 必须为 PixelFormat.Format24bppRgb 格式图像</param>

        /// <returns></returns>

        public static Bitmap WaveDistortion(Bitmap srcBmp, out Color foreground_color)
        {
            foreground_color = Color.Gray;
            if (srcBmp == null)

                return null;

            if (srcBmp.PixelFormat != PixelFormat.Format24bppRgb)

                throw new ArgumentException("srcBmp PixelFormat.Format24bppRgb 格式图像", "srcBmp");
            

            var width = srcBmp.Width;

            var height = srcBmp.Height;
            Random randx = new Random();


            Bitmap destBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            {

                //前景色

                 foreground_color = Color.FromArgb(randx.Next(10, 100), randx.Next(10, 100), randx.Next(10, 100));

                //背景色

                Color background_color = Color.White;// Color.FromArgb(randx.Next(200, 250), randx.Next(200, 250), randx.Next(200, 250));



                using (Graphics newG = Graphics.FromImage(destBmp))
                {

                    newG.Clear(background_color);

                    // periods 时间

                    double rand1 = randx.Next(710000, 1200000) / 10000000.0;

                    double rand2 = randx.Next(710000, 1200000) / 10000000.0;

                    double rand3 = randx.Next(710000, 1200000) / 10000000.0;

                    double rand4 = randx.Next(710000, 1200000) / 10000000.0;

                    // phases  相位

                    double rand5 = randx.Next(0, 31415926) / 10000000.0;

                    double rand6 = randx.Next(0, 31415926) / 10000000.0;

                    double rand7 = randx.Next(0, 31415926) / 10000000.0;

                    double rand8 = randx.Next(0, 31415926) / 10000000.0;

                    // amplitudes 振幅

                    double rand9 = randx.Next(330, 420) / 110.0;

                    double rand10 = randx.Next(330, 450) / 110.0;

                    double amplitudesFactor = randx.Next(5, 6) / 10.0;//振幅小点防止出界

                    double center = width / 2.0;



                    //wave distortion 波纹扭曲

                    BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, destBmp.PixelFormat);

                    BitmapData srcData = srcBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, srcBmp.PixelFormat);

                    for (var x = 0; x < width; x++)
                    {

                        for (var y = 0; y < height; y++)
                        {

                            var sx = x + (Math.Sin(x * rand1 + rand5)

                                        + Math.Sin(y * rand3 + rand6)) * rand9 - width / 2 + center + 1;

                            var sy = y + (Math.Sin(x * rand2 + rand7)

                                        + Math.Sin(y * rand4 + rand8)) * rand10 * amplitudesFactor; //振幅小点防止出界



                            int color, color_x, color_y, color_xy;

                            Color overColor = Color.Empty;



                            if (sx < 0 || sy < 0 || sx >= width - 1 || sy >= height - 1)
                            {

                                continue;

                            }

                            else
                            {

                                color = BitmapDataColorAt(srcData, (int)sx, (int)sy).B;

                                color_x = BitmapDataColorAt(srcData, (int)(sx + 1), (int)sy).B;

                                color_y = BitmapDataColorAt(srcData, (int)sx, (int)(sy + 1)).B;

                                color_xy = BitmapDataColorAt(srcData, (int)(sx + 1), (int)(sy + 1)).B;

                            }



                            if (color == 255 && color_x == 255 && color_y == 255 && color_xy == 255)
                            {

                                continue;

                            }

                            else if (color == 0 && color_x == 0 && color_y == 0 && color_xy == 0)
                            {

                                overColor = Color.FromArgb(foreground_color.R, foreground_color.G, foreground_color.B);

                            }

                            else
                            {

                                double frsx = sx - Math.Floor(sx);

                                double frsy = sy - Math.Floor(sy);

                                double frsx1 = 1 - frsx;

                                double frsy1 = 1 - frsy;



                                double newColor =

                                     color * frsx1 * frsy1 +

                                     color_x * frsx * frsy1 +

                                     color_y * frsx1 * frsy +

                                     color_xy * frsx * frsy;



                                if (newColor > 255) newColor = 255;

                                newColor = newColor / 255;

                                double newcolor0 = 1 - newColor;



                                int newred = Math.Min((int)(newcolor0 * foreground_color.R + newColor * background_color.R), 255);

                                int newgreen = Math.Min((int)(newcolor0 * foreground_color.G + newColor * background_color.G), 255);

                                int newblue = Math.Min((int)(newcolor0 * foreground_color.B + newColor * background_color.B), 255);



                                overColor = Color.FromArgb(newred, newgreen, newblue);

                            }

                            BitmapDataColorSet(destData, x, y, overColor);

                        }

                    }

                    destBmp.UnlockBits(destData);

                    srcBmp.UnlockBits(srcData);

                }

                if (srcBmp != null)

                    srcBmp.Dispose();

            }


            return destBmp;

        }

        /// <summary>

        /// 获得 BitmapData 指定坐标的颜色信息

        /// 实现 PHP imagecolorat

        /// </summary>

        /// <param name="srcData">从图像数据获得颜色 必须为 PixelFormat.Format24bppRgb 格式图像数据</param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <returns>x,y 坐标的颜色数据</returns>

        /// <remarks>

        /// Format24BppRgb 已知X，Y坐标，像素第一个元素的位置为Scan0+(Y*Stride)+(X*3)。

        /// 这是blue字节的位置，接下来的2个字节分别含有green、red数据。

        /// </remarks>

        static Color BitmapDataColorAt(BitmapData srcData, int x, int y)
        {

            if (srcData.PixelFormat != PixelFormat.Format24bppRgb)

                throw new ArgumentException("srcData PixelFormat.Format24bppRgb 格式图像数据", "srcData");



            byte[] rgbValues = new byte[3];

            Marshal.Copy((IntPtr)((int)srcData.Scan0 + ((y * srcData.Stride) + (x * 3))), rgbValues, 0, 3);

            return Color.FromArgb(rgbValues[2], rgbValues[1], rgbValues[0]);

        }

        /// <summary>

        /// 设置 BitmapData 指定坐标的颜色信息

        /// 实现 PHP ImageColorSet

        /// </summary>

        /// <param name="destData">设置图像数据的颜色 必须为 PixelFormat.Format24bppRgb 格式图像数据</param>

        /// <param name="x"></param>

        /// <param name="y"></param>

        /// <param name="color">待设置颜色</param>

        /// <remarks>

        /// Format24BppRgb 已知X，Y坐标，像素第一个元素的位置为Scan0+(Y*Stride)+(X*3)。

        /// 这是blue字节的位置，接下来的2个字节分别含有green、red数据。

        /// </remarks>

        static void BitmapDataColorSet(BitmapData destData, int x, int y, Color color)
        {

            if (destData.PixelFormat != PixelFormat.Format24bppRgb)

                throw new ArgumentException("destData PixelFormat.Format24bppRgb 格式图像数据", "destData");



            byte[] rgbValues = new byte[3] { color.B, color.G, color.R };

            Marshal.Copy(rgbValues, 0, (IntPtr)((int)destData.Scan0 + ((y * destData.Stride) + (x * 3))), 3);

        }

        #endregion
    }
}
