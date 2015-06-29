/*
Author      : 张智
Date        : 2011-3-16
Description : 验证图片功能助手
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web;
using System.Threading;
using Complex.Common.Utility.Extensions;
using Utility;

namespace Complex.Common.Utility.ValidateImage
{
    /// <summary>
    /// 验证图片功能助手
    /// </summary>
    public class ValidateImageHelper
    {

        /// <summary>
        /// 验证图片字符串包括的类型
        /// </summary>
        [Flags()]
        public enum StringType
        {
            /// <summary>
            /// 数字
            /// </summary>
            Number = 1,
            /// <summary>
            /// 大写字母
            /// </summary>
            UpperCase = 2,
            /// <summary>
            /// 小写字母
            /// </summary>
            LowerCase = 4,
            /// <summary>
            /// 中文
            /// </summary>
            Chinese = 8
        }

        #region 常量

        /// <summary>
        /// 普通的字符类型
        /// </summary>
        const StringType NORMAL_STRINGTYPE = StringType.Number | StringType.LowerCase | StringType.UpperCase;

        /// <summary>
        /// 500个最常用的汉字
        /// </summary>
        public const string NORMAL_CHINESE = "的一是在不了有和人这中大为上个国我以要他工也能下过子说产种面而方后多定行学法所民得时来用们生到作地于出就分对成会可主发年动同经十三之进着等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必 战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往酸历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局照参红细引听该铁价严";

        /// <summary>
        /// 数字
        /// </summary>
        public const string NUMBERS = "23456789";

        /// <summary>
        /// 大写字母
        /// </summary>
        public const string UPPER_LETTER = "ABCDEFGHJKMNPQRSTUVWXYZ";

        /// <summary>
        /// 小写字母
        /// </summary>
        public const string LOWER_LETTER = "abcdefghjkmnpqrstuvwxyz";
        #endregion

        #region 私有成员

        const int DEFAULT_FONT_SIZE = 20;
        const string DEFAULT_FONT_FAMILY = "arial";

        #endregion

        /// <summary>
        /// 验证图片生成器
        /// </summary>
        public IValidateImageGenerator Generator
        {
            get;
            private set;
        }

        /// <summary>
        /// 用默认的验证图片生成器创建一个验证图片功能助手
        /// </summary>
        public ValidateImageHelper() :
            this(new DefaultValidateImageGenerator())
        {

        }

        /// <summary>
        /// 用指定的验证图片生成器创建一个验证图片功能助手
        /// </summary>
        /// <param name="generator"></param>
        public ValidateImageHelper(IValidateImageGenerator generator)
        {
            this.Generator = generator;
        }

        /// <summary>
        /// 创建一个验证图片助手
        /// </summary>
        /// <returns></returns>
        public static ValidateImageHelper Create()
        {
            return new ValidateImageHelper();
        }

        /// <summary>
        /// 用指定的验证图片生成器创建一个验证图片助手
        /// </summary>
        /// <typeparam name="TValidateImageGenerator">验证图片生成器类型</typeparam>
        /// <returns></returns>
        public static ValidateImageHelper Create<TValidateImageGenerator>()
            where TValidateImageGenerator : IValidateImageGenerator, new()
        {
            return new ValidateImageHelper(new TValidateImageGenerator());
        }

        /// <summary>
        /// 获得N长度的随机字符串
        /// </summary>
        /// <param name="codeType">字符串中包含的字符类型</param>
        /// <param name="n">字符串的长度</param>
        /// <returns></returns>
        public static string GetRandomText(StringType codeType, int n)
        {
            if (n < 1)
                return string.Empty;

            HashSet<StringType> stringTypeSet = new HashSet<StringType>();
            if ((codeType & StringType.Chinese) == StringType.Chinese)
            {
                stringTypeSet.Add(StringType.Chinese);
            }
            if ((codeType & StringType.LowerCase) == StringType.LowerCase)
            {
                stringTypeSet.Add(StringType.LowerCase);
            }
            if ((codeType & StringType.Number) == StringType.Number)
            {
                stringTypeSet.Add(StringType.Number);
            }
            if ((codeType & StringType.UpperCase) == StringType.UpperCase)
            {
                stringTypeSet.Add(StringType.UpperCase);
            }

            if (stringTypeSet.Count < 1)
                return string.Empty;

            int el = n / stringTypeSet.Count;

            if (el == 0)
                el = 1;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringType last = 0;
            if (stringTypeSet.Contains(StringType.Chinese))
            {
                sb.Append(GetRandomText(NORMAL_CHINESE, el));
                last = StringType.Chinese;
            }

            if (stringTypeSet.Contains(StringType.LowerCase))
            {
                sb.Append(GetRandomText(LOWER_LETTER, el));
                last = StringType.LowerCase;
            }
            if (stringTypeSet.Contains(StringType.Number))
            {
                sb.Append(GetRandomText(NUMBERS, el));
                last = StringType.Number;
            }
            if (stringTypeSet.Contains(StringType.UpperCase))
            {
                sb.Append(GetRandomText(UPPER_LETTER, el));
                last = StringType.UpperCase;
            }

            int pad = n - sb.Length;
            if (pad > 0)
            {
                sb.Append(GetRandomText(last, pad));
            }

            return sb.ToString().Left(n);
        }

        /// <summary>
        /// 获得指定字符串中N个随机的字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="n">新字符串的长度</param>
        /// <returns></returns>
        public static string GetRandomText(string str, int n)
        {
            Thread.Sleep(1); //延迟一毫秒 防止时间种子相同

            Random randomGenerator = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int length = str.Length;
            for (int i = 0; i < n; i++)
            {
                int loc = randomGenerator.Next(0, length);
                sb.Append(str[loc]);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 生成给定字符串的验证图片 并且可以设置字体
        /// </summary>
        /// <param name="str">要生成的字符串</param>
        /// <param name="font">生成的字体</param>
        /// <returns></returns>
        public Image GetImage(string str, Font font)
        {
            return this.Generator.GenerateImage(str, font);
        }

        /// <summary>
        /// 生成给定字符串的验证图片
        /// </summary>
        /// <param name="str">要生成的字符串</param>
        /// <returns></returns>
        public Image GetImage(string str)
        {
            return GetImage(str, DEFAULT_FONT_SIZE);
        }

        /// <summary>
        /// 生成给定字符串的验证图片 并且可以设置字体的像素大小
        /// </summary>
        /// <param name="str">要生成的字符串</param>
        /// <param name="fontSize">字体像素大小</param>
        /// <returns></returns>
        public Image GetImage(string str, int fontSize)
        {
            using (Font font = new Font(DEFAULT_FONT_FAMILY, (float)fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                return GetImage(str, font);
            }
        }

        /// <summary>
        /// 自动产生字符串 并且生成相应的验证图片
        /// </summary>
        /// <param name="stringType">字符串中包含的字符类型</param>
        /// <param name="length">字符串的长度</param>
        /// <param name="font">生成的字体</param>
        /// <param name="str">自动产生的字符串</param>
        /// <returns></returns>
        public Image GetImage(StringType stringType, int length, Font font, out string str)
        {
            str = GetRandomText(stringType, length);
            return GetImage(str, font);
        }

        /// <summary>
        /// 自动产生字符串 并且生成相应的验证图片
        /// </summary>
        /// <param name="stringType">字符串中包含的字符类型</param>
        /// <param name="length">字符串的长度</param>
        /// <param name="fontSize">生成的字体的像素大小</param>
        /// <param name="str">自动产生的字符串</param>
        /// <returns></returns>
        public Image GetImage(StringType stringType, int length, int fontSize, out string str)
        {
            using (Font font = new Font(DEFAULT_FONT_FAMILY, (float)fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                return GetImage(stringType, length, font, out str);
            }
        }



        /// <summary>
        /// 自动产生字符串 并且生成相应的验证图片
        /// </summary>
        /// <param name="stringType">字符串中包含的字符类型</param>
        /// <param name="length">字符串的长度</param>
        /// <param name="fontSize">生成的字体的像素大小</param>
        /// <param name="str">自动产生的字符串</param>
        /// <returns></returns>
        public Image GetImage(StringType stringType, int length, out string str)
        {
            return GetImage(stringType, length, DEFAULT_FONT_SIZE, out str);
        }

        /// <summary>
        /// 生成简单的四个数字组成的验证图片
        /// </summary>
        /// <param name="str">自动产生的字符串</param>
        /// <returns></returns>
        public Image GetSimpleImage(out string str)
        {
            str = GetRandomText(NUMBERS, 4);
            return GetImage(str);
        }

        /// <summary>
        /// 生成普通的由大小写字母 数字 组成的四个字符的验证图片
        /// </summary>
        /// <param name="str">自动产生的字符串</param>
        /// <returns></returns>
        public Image GetNormalImage(out string str)
        {
            str = GetRandomText(NORMAL_STRINGTYPE, 4);
            return GetImage(str);
        }

        /// <summary>
        /// 生成给定字符串的验证图片 并将其以指定的图像格式保持到流中
        /// </summary>
        /// <param name="stream">图片保存的流</param>
        /// <param name="imageFormat">图像格式</param>
        /// <param name="str">要生成的字符串</param>
        /// <param name="font">生成的字体</param>
        public void GeneratorImageAndSave(Stream stream, ImageFormat imageFormat, string str, Font font)
        {
            using (Image image = GetImage(str, font))
            {
                using (EncoderParameters eps = new EncoderParameters(1))
                {
                    eps.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    image.Save(stream, ImageUtil.EncoderGuidMap[imageFormat.Guid], eps);
                }
            }
        }

        /// <summary>
        ///  生成普通的由大小写字母 数字 组成的四个字符的验证图片 并将其以指定的图像格式保持到流中
        /// </summary>
        /// <param name="stream">图片保存的流</param>
        /// <param name="imageFormat">图像格式</param>
        /// <param name="font">生成的字体</param>
        /// <returns>自动生成的字符串</returns>
        public string GeneratorNormalImageAndSave(Stream stream, ImageFormat imageFormat, Font font)
        {
            string str = GetRandomText(NORMAL_STRINGTYPE, 4);
            GeneratorImageAndSave(stream, imageFormat, str, font);
            return str;
        }

        /// <summary>
        /// 自动生成4个数字组成的字符串的验证图片 并将其以指定的图像格式保持到流中
        /// </summary>
        /// <param name="stream">图片保存的流</param>
        /// <param name="imageFormat">图像格式</param>
        /// <param name="font">生成的字体</param>
        /// <returns>自动生成的字符串</returns>
        public string GeneratorSimpleImageAndSave(Stream stream, ImageFormat imageFormat, Font font)
        {
            string str = GetRandomText(NUMBERS, 4);
            GeneratorImageAndSave(stream, imageFormat, str, font);
            return str;
        }

        /// <summary>
        ///  生成普通的由大小写字母 数字 组成的四个字符的验证图片 并将其以gif格式保存到流中
        /// </summary>
        /// <param name="stream">图片保存的流</param>
        /// <returns></returns>
        public string GeneratorNormalImageAndSave(Stream stream)
        {
            using (Font font = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                return GeneratorNormalImageAndSave(stream, ImageFormat.Gif, font);
            }
        }

        /// <summary>
        ///  自动生成4个数字组成的字符串的验证图片 并将其以gif格式保存到流中
        /// </summary>
        /// <param name="stream">图片保存的流</param>
        /// <returns></returns>
        public string GeneratorSimpleImageAndSave(Stream stream)
        {
            using (Font font = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                return GeneratorSimpleImageAndSave(stream, ImageFormat.Gif, font);
            }
        }

        /// <summary>
        /// 生成指定的验证图片 并将其以gif格式保存到当前HTTP响应流中
        /// </summary>
        /// <param name="stringType">字符串中包含的字符类型</param>
        /// <param name="length">字符串的长度</param>
        /// <param name="fontSize">生成的字体的像素大小</param>
        /// <param name="str">自动产生的字符串</param>
        /// <returns></returns>
        public string GetImageAndSave(StringType stringType, int length, int fontSize)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
                throw new NullReferenceException("当前HTTP上下文为空");

            string str = GetRandomText(stringType, length);
            using (Font font = new Font(DEFAULT_FONT_FAMILY, fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                GeneratorImageAndSave(current.Response.OutputStream, ImageFormat.Jpeg, str, font);
            }

            return str;
        }
        /// <summary>
        /// 按传入的字符串 生成验证码
        /// </summary>
        /// <param name="str">传入的字符串</param>
        /// <param name="fontSize">字符大小</param>
        public void GetImageAndSave(string str, int fontSize)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
                throw new NullReferenceException("当前HTTP上下文为空");
            using (Font font = new Font(DEFAULT_FONT_FAMILY, fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                GeneratorImageAndSave(current.Response.OutputStream, ImageFormat.Jpeg, str, font);
            }
        }

        /// <summary>
        ///  生成普通的由大小写字母 数字 组成的四个字符的验证图片 并将其以gif格式保存到当前HTTP响应流中
        /// </summary>
        /// <returns></returns>
        public string GeneratorNormalImageAndSave()
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
                throw new NullReferenceException("当前HTTP上下文为空");

            return GeneratorNormalImageAndSave(current.Response.OutputStream);
        }

        /// <summary>
        ///  自动生成4个数字组成的字符串的验证图片 并将其以gif格式保存到当前HTTP响应流中
        /// </summary>
        /// <returns></returns>
        public string GeneratorSimpleImageAndSave()
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
                throw new NullReferenceException("当前HTTP上下文为空");

            return GeneratorSimpleImageAndSave(current.Response.OutputStream);
        }

    }
}
