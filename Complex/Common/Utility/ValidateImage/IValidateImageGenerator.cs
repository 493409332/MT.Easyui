/*
Author      : 张智
Date        : 2011-3-16
Description : 提供验证图片生成的功能
*/

using System.Drawing;

namespace Complex.Common.Utility.ValidateImage
{
    /// <summary>
    /// 提供验证图片生成器
    /// </summary>
    public interface IValidateImageGenerator
    {
        /// <summary>
        /// 产生验证图片
        /// </summary>
        /// <param name="text">验证图片中要生成的文字</param>
        /// <param name="font">文字的字体</param>
        /// <returns></returns>
        Image GenerateImage(string text, Font font);
    }
}
