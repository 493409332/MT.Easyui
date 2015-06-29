/*
Author      : 沈进坤
Date        : 2011-6-10
Description : 根据不同语言获取不同提示信息帮助类
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using Utility;

namespace Complex.Common.Utility.Validate
{
    internal class LanguageAttribute : System.Attribute
    {
        public string ZH_CN { get; set; }
        public string ZH_TW { get; set; }
        public string EN { get; set; }
    }

    internal enum TipInfo
    {
        [Language(ZH_CN = "{0}不可为空字符串", EN = "{0} can not be an null string.")]
        STR_NOT_EMPTY,

        [Language(ZH_CN = "{0}的长度不可超过{1}", EN = "the length of {0} doesn't exceed {1}")]
        STR_OVER_LENGTH,

        [Language(ZH_CN = "{0}的长度不可小于{1}", EN = "the length of {0} is not less than {1}.")]
        STR_LESS_LENGTH,

        [Language(ZH_CN = "{0}与{1}不一致", EN = "{0} and {1} are inconsistent")]
        STR_NOT_EQUAL,


        [Language(ZH_CN = "{0}的长度必须在{1}和{2}之间", EN = "The length of {0} must be in between {1} and {2}.")]
        STR_LENGTH_RANGE,

        [Language(ZH_CN = "{0}存在非法字符", EN = "There is an illegal character in {0}.")]
        STR_NOT_SAFE,

        [Language(ZH_CN = "{0}不是合法的IP地址", EN = "{0} is invalid IP address.")]
        STR_ISIP,

        [Language(ZH_CN = "{0}不是合法的Email", EN = "{0} is invalid Eamil.")]
        STR_ISEMAIL,

        [Language(ZH_CN = "{0}不是有效的链接(URL)地址", EN = "{0} is invalid URL address.")]
        STR_ISURL,

        [Language(ZH_CN = "{0}不是有效的日期数据", EN = "{0} is invalid date data.")]
        STR_ISDATETIME,

        [Language(ZH_CN = "{0}不是有效的身份证号", EN = "{0} is invalid ID number.")]
        STR_ISIDCARD,

        [Language(ZH_CN = "{0}含有非中文字符", EN = "{0} contains non-Chinese characters")]
        STR_ISCHINESE,

        [Language(ZH_CN = "{0}不是有效的电话号码", EN = "{0} is invalid telephone number.")]
        STR_ISPHONE,

        [Language(ZH_CN = "{0}不能小于{1}", EN = "{0} can not be less than {1}.")]
        INT_LESS,

        [Language(ZH_CN = "{0}不能大于{1}", EN = "{0} can not be more than {1}")]
        INT_OVERFLOW,

        [Language(ZH_CN = "{0}不能大于{1}，且不能小于{2}", EN = "{0} can not be more than {1},and not less than {2}.")]
        INT_RANGE
    }

    internal class GetTipLanguage {

        static IDictionary<TipInfo, IDictionary<Language, string>> dic = new Dictionary<TipInfo, IDictionary<Language, string>>();

        internal static string Get(TipInfo tip, Language lang) {            
            if (dic.ContainsKey(tip) && dic[tip].ContainsKey(lang))
                return dic[tip][lang];                       

            Type _enumType = typeof(TipInfo);

            FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, tip));  
            LanguageAttribute[] ds = (LanguageAttribute[])fi.GetCustomAttributes(typeof(LanguageAttribute), false);
            if (ds == null || ds.Length==0)
                return string.Empty;
            else {
                string str = string.Empty;
                LanguageAttribute la = ds[0];
                switch (lang) {
                    case Language.ZH_CN: str = la.ZH_CN; break;
                    case Language.ZH_TW: str = la.ZH_TW; break;
                    case Language.EN: str = la.EN; break;
                    default: break;
                }
                if (!dic.ContainsKey(tip))
                    dic[tip] = new Dictionary<Language, string>();
                dic[tip][lang] = str;
                return str;
            }
        }
    }
}
