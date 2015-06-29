using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace Complex.Common.Utility
{

    public class Bases
    {
        /// <summary>
        /// 截取字符长，超过部分以 ...显示
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetPartString(string Str, int Num)
        {
            if (Str.Length <= Num)
                return Str;
            return Str.Substring(0, Num - 1) + "&#8230;&#8230;";
        }
        /// <summary>
        /// 判断一个对象是否为数字（无论是int，double，float等等）。FY
        /// </summary>
        /// <param name="o">检查的对象</param>
        /// <returns> 如果是数字返回true ，不是数字返回false</returns>
        public static bool IsNumeric(object o)
        {
            try
            {
                Double.Parse(o.ToString());
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 对象是否非空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <returns>bool值</returns>
        public static bool NotNull(object Object) { return !IsNull(Object, false); }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static bool NotNull(string obj) { return false; }
        /// <summary>
        /// 对象是否非空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsRemoveSpace">是否去除空格</param>
        /// <returns>bool值</returns>
        public static bool NotNull(object Object, bool IsRemoveSpace) { return !IsNull(Object, IsRemoveSpace); }
        /// <summary>
        /// 对象是否为空
        /// 为空返回 false
        /// 不为空返回 true
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <returns>bool值</returns>
        public static bool IsNull(object Object) { return IsNull(Object, false); }
        /// <summary>
        /// 对象是否为空
        /// 为空返回 true
        /// 不为空返回 false
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsRemoveSpace">是否去除空格</param>
        /// <returns>bool值</returns>
        public static bool IsNull(object Object, bool IsRemoveSpace)
        {
            if (Object == null) return true;
            string Objects = Object.ToString();
            if (Objects == "") return true;
            if (IsRemoveSpace)
            {
                if (Objects.Replace(" ", "") == "") return true;
                if (Objects.Replace("　", "") == "") return true;
            }
            return false;
        }
        /// <summary>
        /// 对象是否为bool值
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <returns>bool值</returns>
        public static bool IsBool(object Object) { return IsBool(Object, false); }
        /// <summary>
        /// 判断是否为bool值
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="Default">默认bool值</param>
        /// <returns>bool值</returns>
        public static bool IsBool(object Object, bool Default)
        {
            if (IsNull(Object)) return Default;
            try { return bool.Parse(Object.ToString()); }
            catch { return Default; }
        }

        ///// <summary>
        ///// 是否邮件地址
        ///// </summary>
        ///// <param name="Mail">等待验证的邮件地址</param>
        ///// <returns>bool</returns>
        //public static bool NoMail(string Mail) { return Regex.IsMatch(Mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); }
        /// <summary>
        /// 是否邮件地址
        /// </summary>
        /// <param name="Mail">等待验证的邮件地址</param>
        /// <returns>bool</returns>
        public static bool IsMail(string Mail) { return Regex.IsMatch(Mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); }
        /// <summary>
        /// 是否URL地址
        /// </summary>
        /// <param name="HttpUrl">等待验证的Url地址</param>
        /// <returns>bool</returns>
        public static bool IsHttp(string HttpUrl) { return Regex.IsMatch(HttpUrl, @"^(http|https):\/\/[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]{2,}[A-Za-z0-9\.\/=\?%\-&_~`@[\]:+!;]*$"); }
        /// <summary>
        /// 取字符左函数
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string Left(object Object, int MaxLength)
        {
            if (IsNull(Object)) return "";
            return Object.ToString().Substring(0, Math.Min(Object.ToString().Length, MaxLength));
        }
        /// <summary>
        /// 取字符中间函数
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="StarIndex">开始的位置索引</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string Mid(string Object, int StarIndex, int MaxLength)
        {
            if (IsNull(Object)) return "";
            if (StarIndex >= Object.Length) return "";
            return Object.Substring(StarIndex, MaxLength);
        }
        /// <summary>
        /// 取字符右函数
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string Right(object Object, int MaxLength)
        {
            if (IsNull(Object)) return "";
            int i = Object.ToString().Length;
            if (i < MaxLength) { MaxLength = i; i = 0; } else { i = i - MaxLength; }
            return Object.ToString().Substring(i, MaxLength);
        }
       
        /// <summary>
        /// 关键字上色处理
        /// </summary>
        /// <param name="Object">要操作的 string  数据</param>
        /// <param name="Keys">关键字</param>
        /// <param name="Style">样式名称</param>
        /// <returns>string</returns>
        public static string AddColors(string Object, string Keys, string Style)
        {
            StringBuilder Builders = new StringBuilder(Object);
            Builders.Replace(Keys, "<span class=\"" + Style + "\">" + Keys + "</span>");
            return Builders.ToString();
        }
        /// <summary>
        /// 数字前导加0
        /// </summary>
        /// <param name="Int">要操作的 int  数据</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string AddZeros(int Int, int MaxLength) { return AddZeros(Int.ToString(), MaxLength); }
        /// <summary>
        /// 数字前导加0
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="MaxLength">默认长度不加0</param>
        /// <returns>字符</returns>
        public static string AddZeros(string Object, int MaxLength)
        {
            int iLength = Object.Length;
            if (iLength < MaxLength)
            {
                for (int i = 1; i <= (MaxLength - iLength); i++) Object = "0" + Object;
            }
            return Object;
        }
        /// <summary>
        /// 重新获取所有ID排序
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <returns>字符</returns>
        public static string ToLoadID(string Object)
        {
            if (IsNull(Object)) return "";
            StringBuilder Builder = new StringBuilder("");
            string[] Atemp = Object.Split(',');
            for (int i = 0; i < Atemp.Length; i++)
            {
                bool IsTrue = false; int Int = IsInt(Atemp[i].ToString(), out IsTrue);
                if (IsTrue) Builder.Append(((i != 0) ? "," : "") + Int.ToString());
            }
            return Builder.ToString();
        }
        /// <summary>
        /// 字符 int  转换为 char
        /// </summary>
        /// <param name="Int">字符[int]</param>
        /// <returns>char</returns>
        public static char IntToChar(int Int) { return (char)Int; }
        /// <summary>
        /// 字符 int  转换为字符 string
        /// </summary>
        /// <param name="Int">字符 int</param>
        /// <returns>字符 string</returns>
        public static string IntToString(int Int) { return IntToChar(Int).ToString(); }
        /// <summary>
        /// 字符 string  转换为字符 int
        /// </summary>
        /// <param name="Strings">字符 string</param>
        /// <returns>字符 int</returns>
        public static int StringToInt(string Strings)
        {
            char[] chars = Strings.ToCharArray(); return (int)chars[0];
        }
        /// <summary>
        /// 字符 string  转换为 char
        /// </summary>
        /// <param name="Strings">字符 string</param>
        /// <returns>char</returns>
        public static char StringToChar(string Strings) { return IntToChar(StringToInt(Strings)); }
        /// <summary>
        /// 对象是否为 int  类型数据
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsTrue">返回是否转换成功</param>
        /// <returns>int值</returns>
        private static int IsInt(object Object, out bool IsTrue)
        {
            if (IsNull(Object)) { IsTrue = false; return 0; }
            try { IsTrue = true; return int.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// 转换成为 int  数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <returns>int 数据</returns>
        public static int ToInt(object Object) { return ToInt(Object, 0); }
        /// <summary>
        /// 转换成为 int  数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <returns>int 数据</returns>
        public static int ToInt(object Object, int Default) { return ToInt(Object, Default, 0, 999999999); }
        /// <summary>
        /// 转换成为 int  数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinInt"> 下界限定的最小值 , 若超过范围 , 则返回 默认值</param>
        /// <returns>int 数据</returns>
        public static int ToInt(object Object, int Default, int MinInt) { return ToInt(Object, Default, MinInt, 999999999); }
        /// <summary>
        /// 转换成为 int  数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinInt"> 下界限定的最小值 , 若超过范围 , 则返回 默认值</param>
        /// <param name="MaxInt">上界限定的最大值 , 若超过范围 , 则返回 默认值</param>
        /// <returns>int 数据</returns>
        public static int ToInt(object Object, int Default, int MinInt, int MaxInt)
        {
            bool IsTrue = false;
            int Int = IsInt(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Int < MinInt || Int > MaxInt) return Default;
            return Int;
        }

        /// <summary>
        /// 对象是否为 long  类型数据
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsTrue">返回是否转换成功</param>
        /// <returns>long值</returns>
        private static long IsLong(object Object, out bool IsTrue)
        {
            if (IsNull(Object)) { IsTrue = false; return 0; }
            try { IsTrue = true; return long.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// 转换成为 Long 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <returns>Long 数据</returns>
        public static long ToLong(object Object) { return ToLong(Object, 0); }
        /// <summary>
        /// 转换成为 Long 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <returns>Long 数据</returns>
        public static long ToLong(object Object, long Default) { return ToLong(Object, Default, -9223372036854775808, 9223372036854775807); }
        /// <summary>
        /// 转换成为 long 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">转换不成功返回的默认值</param>
        /// <param name="MinLong">下界限定的最小值 , 若超过范围 , 则返回 默认值</param>
        /// <returns>long 数据</returns>
        public static long ToLong(object Object, long Default, long MinLong) { return ToLong(Object, Default, MinLong, 9223372036854775807); }
        /// <summary>
        /// 转换成为 long 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinLong">下界限定的最小值 , 若超过范围 , 则返回 默认值</param>
        /// <param name="MaxLong">上界限定的最大值 , 若超过范围 , 则返回 默认值</param>
        /// <returns>long 数据</returns>
        public static long ToLong(object Object, long Default, long MinLong, long MaxLong)
        {
            bool IsTrue = false;
            long Long = IsLong(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Long < MinLong || Long > MaxLong) return Default;
            return Long;
        }
        /// <summary>
        /// 对象是否为 float  类型数据
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsTrue">返回是否转换成功</param>
        /// <returns>float值</returns>
        private static float IsFloat(object Object, out bool IsTrue)
        {
            if (IsNull(Object)) { IsTrue = false; return 0; }
            try { IsTrue = true; return float.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// 转换成为 float 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <returns>float 数据</returns>
        public static float ToFloat(object Object) { return ToFloat(Object, 0); }
        /// <summary>
        /// 转换成为 float 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <returns>float 数据</returns>
        public static float ToFloat(object Object, float Default) { return ToFloat(Object, Default, 0, 999999999); }
        /// <summary>
        /// 转换成为 float 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinFloat"> 小于等于 转换成功后,下界限定的最小值,若超过范围 则返回 默认值</param>
        /// <returns>float 数据</returns>
        public static float ToFloat(object Object, float Default, float MinFloat) { return ToFloat(Object, Default, MinFloat, 999999999); }
        /// <summary>
        /// 转换成为 float 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinFloat"> 下界限定的最小值 , 若超过范围 , 则返回 默认值</param>
        /// <param name="MaxFloat"> 上界限定的最大值 , 若超过范围 , 则返回 默认值</param>
        /// <returns>float 数据</returns>
        public static float ToFloat(object Object, float Default, float MinFloat, float MaxFloat)
        {
            bool IsTrue = false;
            float Float = IsFloat(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Float < MinFloat || Float > MaxFloat) return Default;
            return Float;
        }
        /// <summary>
        /// 对象是否为 decimal  类型数据
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsTrue">返回是否转换成功</param>
        /// <returns>decimal值</returns>
        private static decimal IsDecimal(object Object, out bool IsTrue)
        {
            if (IsNull(Object)) { IsTrue = false; return 0; }
            try { IsTrue = true; return decimal.Parse(Object.ToString()); }
            catch { IsTrue = false; return 0; }
        }
        /// <summary>
        /// 转换成为 decimal 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <returns>decimal 数据</returns>
        public static decimal ToDecimal(object Object) { return ToDecimal(Object, 0); }
        /// <summary>
        /// 转换成为 decimal 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <returns>decimal 数据</returns>
        public static decimal ToDecimal(object Object, decimal Default) { return ToDecimal(Object, Default, 0, 999999999); }

        /// <summary>
        /// 转换成为 decimal 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinFloat"> 小于等于 转换成功后,下界限定的最小值,若超过范围 则返回 默认值</param>
        /// <returns>decimal 数据</returns>
        public static decimal ToDecimal(object Object, decimal Default, decimal MinFloat) { return ToDecimal(Object, Default, MinFloat, 999999999); }
        /// <summary>
        /// 转换成为 decimal 数据
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="Default">默认值</param>
        /// <param name="MinDecimal"> 下界限定的最小值 , 若超过范围 , 则返回 默认值</param>
        /// <param name="MaxDecimal"> 上界限定的最大值 , 若超过范围 , 则返回 默认值</param>
        /// <returns>decimal 数据</returns>
        public static decimal ToDecimal(object Object, decimal Default, decimal MinDecimal, decimal MaxDecimal)
        {
            bool IsTrue = false;
            decimal Decimal = IsDecimal(Object, out IsTrue);
            if (!IsTrue) return Default;
            if (Decimal < MinDecimal || Decimal > MaxDecimal) return Default;
            return Decimal;
        }
        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <param name="IsTrue">返回是否转换成功</param>
        /// <returns>DateTime</returns>
        public static DateTime IsTime(object Object, out bool IsTrue)
        {
            IsTrue = false;
            if (IsNull(Object)) return DateTime.Now;
            try { IsTrue = true; return DateTime.Parse(Object.ToString()); }
            catch { IsTrue = false; return DateTime.Now; }
        }

        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <param name="Object">要判断的对象</param>
        /// <returns>Bool值</returns>
        public static bool IsTime(object Object)
        {
            if (IsNull(Object)) return false;
            try { DateTime.Parse(Object.ToString()); return true; }
            catch { return false; }
        }

        /// <summary>
        /// 操作 DateTime  数据
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <returns>DateTime</returns>
        public static DateTime ToTime(string Object) { return ToTime(Object, DateTime.Now); }
        /// <summary>
        /// 字符串转换为时间函数
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <param name="Default">默认时间</param>
        /// <returns>DateTime</returns>
        public static DateTime ToTime(string Object, DateTime Default)
        {
            if (IsNull(Object)) return Default;
            bool IsTrue = false;
            DateTime Time = IsTime(Object, out IsTrue);
            if (!IsTrue) return Default;
            return Time;
        }
        /// <summary>
        /// 获得当前时间
        /// </summary>
        /// <param name="Style">时间样式</param>
        /// <returns>string</returns>
        public static string ToNow(string Style) { return DateTime.Now.ToString(Style); }
        /// <summary>
        /// 转换字符串为格式化时间字符串
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object) { return ToTimes(Object, "yyyy-MM-dd HH:mm:ss"); }
        /// <summary>
        /// 转换字符串为格式化时间字符串
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <param name="Style">格式化样式</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object, string Style) { return ToTimes(Object, DateTime.Now, Style); }
        /// <summary>
        /// 转换字符串为格式化时间字符串
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <param name="Default">默认时间</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object, DateTime Default) { return ToTimes(Object, Default, "yyyy-MM-dd HH:mm:ss"); }
        /// <summary>
        /// 转换字符串为格式化时间字符串
        /// </summary>
        /// <param name="Object">要操作的字符</param>
        /// <param name="Default">默认时间</param>
        /// <param name="Style">格式化样式</param>
        /// <returns>string</returns>
        public static string ToTimes(string Object, DateTime Default, string Style)
        {
            if (IsNull(Object)) return Default.ToString(Style);
            bool IsTrue = false;
            DateTime Time = IsTime(Object, out IsTrue);
            if (!IsTrue) return Default.ToString(Style);
            return Time.ToString(Style);
        }
        /// <summary>
        /// 转换 DateTime 对象为格式化时间字符串
        /// </summary>
        /// <param name="Object">要操作的 DateTime 对象</param>
        /// <returns>string</returns>
        public static string ToTimes(DateTime Object) { return ToTimes(Object, "yyyy-MM-dd HH:mm:ss"); }
        /// <summary>
        /// 转换 DateTime 对象为格式化时间字符串
        /// </summary>
        /// <param name="Object">要操作的 DateTime 对象</param>
        /// <param name="Style">格式化样式</param>
        /// <returns>string</returns>
        public static string ToTimes(DateTime Object, string Style) { return Object.ToString(Style); }
        /// <summary>
        /// 正则搜索 返回 指定Match索引 0  string 索引 1 的 string
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <returns>string</returns>
        public static string SearchTxt(string Pattern, string SearchString)
        {
            return SearchTxt(Pattern, SearchString, 0, 1);
        }
        /// <summary>
        /// 正则搜索 返回 指定索引 1 的 string
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <param name="MatchIndex">指定返回的索引Match</param>
        /// <returns>string</returns>
        public static string SearchTxt(string Pattern, string SearchString, int MatchIndex)
        {
            return SearchTxt(Pattern, SearchString, MatchIndex, 1);
        }
        /// <summary>
        /// 正则搜索 返回 指定索引 string
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <param name="MatchIndex">指定返回的索引Match</param>
        /// <param name="Index">指定索引</param>
        /// <returns>string</returns>
        public static string SearchTxt(string Pattern, string SearchString, int MatchIndex, int Index)
        {
            string[] Txts = SearchMatchTxts(Pattern, SearchString, MatchIndex);
            if (Txts == null) return "";
            if (Index > Txts.Length) return "";
            string Txt = Txts[Index];
            Txts = null;
            return Txt;
        }
        /// <summary>
        /// 正则搜索 返回 不做任何操作的 string[] 
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <returns>string[]</returns>
        public static string[] SearchMatchTxts(string Pattern, string SearchString)
        {
            return SearchMatchTxts(Pattern, SearchString, 0);
        }
        /// <summary>
        /// 正则搜索 返回 string[] 找到的集合
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <param name="MatchIndex">指定返回的索引Match</param>
        /// <returns>string[]</returns>
        public static string[] SearchMatchTxts(string Pattern, string SearchString, int MatchIndex)
        {
            Match Ma = SearchMatch(Pattern, SearchString, MatchIndex);
            if (Ma == null) return null;
            int i = 0;
            string[] Txts = new string[Ma.Groups.Count];
            foreach (Group Gp in Ma.Groups) { Txts[i] = Gp.Value; i++; }
            return Txts;
        }
        /// <summary>
        /// 正则搜索 返回 Object[] 找到的集合
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <returns>Object[], Object 为string[] 对象</returns>
        public static Object[] SearchMatchsTxts(string Pattern, string SearchString)
        {
            MatchCollection Mas = SearchMatchs(Pattern, SearchString);
            if (Mas == null) return null;
            Object[] Objs = new Object[Mas.Count];
            int i = 0;
            foreach (Match Ma in Mas)
            {
                int k = 0;
                string[] Txts = new string[Ma.Groups.Count];
                foreach (Group Gp in Ma.Groups) { Txts[k] = Gp.Value; k++; }
                Objs[i] = Txts;
                i++;
            }
            Mas = null;
            return Objs;
        }
        /// <summary>
        /// 正则搜索 返回 Match
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <param name="MatchIndex">指定返回的索引Match</param>
        /// <returns>Match</returns>
        public static Match SearchMatch(string Pattern, string SearchString, int MatchIndex)
        {
            MatchCollection Mas = SearchMatchs(Pattern, SearchString);
            if (Mas == null) return null;
            if (MatchIndex > Mas.Count - 1) return null;
            Match Ma = Mas[MatchIndex];
            Mas = null;
            return Ma;
        }
        /// <summary>
        /// 正则搜索 返回 Match 集合
        /// </summary>
        /// <param name="Pattern">规则</param>
        /// <param name="SearchString">要搜索的文本</param>
        /// <returns>MatchCollection</returns>
        public static MatchCollection SearchMatchs(string Pattern, string SearchString)
        {
            Regex Reg = new Regex(Pattern, RegexOptions.IgnoreCase);
            MatchCollection Mas = Reg.Matches(SearchString);
            if (Mas.Count <= 0) return null;
            Reg = null;
            return Mas;
        }
        /// <summary>
        /// 判断权限 返回当前索引的权限的数字
        /// </summary>
        /// <param name="PowerChars">权限数组</param>
        /// <param name="Index">当前权限数组中某个权限的索引</param>
        /// <returns>int</returns>
        public static int ToPowerValue(char[] PowerChars, int Index)
        {
            if (Index < 0) return 1;
            if (PowerChars == null) return 0;
            if (Index >= PowerChars.Length) return 0;
            return (PowerChars[Index] == '0') ? 0 : 1;
        }
        /// <summary>
        /// 获取当前权限数组中某个索引的权限
        /// </summary>
        /// <param name="PowerChars">权限数组</param>
        /// <param name="Index">当前权限数组中某个权限的索引</param>
        /// <returns>bool</returns>
        public static bool IsPower(char[] PowerChars, int Index) { return ((ToPowerValue(PowerChars, Index) == 0) ? false : true); }
        /// <summary>
        /// 获取当前权限字符串中某个索引的权限
        /// </summary>
        /// <param name="Powers">权限字符串 101110000的形式</param>
        /// <param name="Index">当前权限数组中某个权限的索引</param>
        /// <returns>bool</returns>
        public static bool IsPower(string Powers, int Index)
        {
            if (IsNull(Powers)) return false;
            return IsPower(Powers.ToCharArray(), Index);
        }
        /// <summary>
        /// 将权限数组切换到权限字符串
        /// </summary>
        /// <param name="PowerChars">PowerChars 权限数组</param>
        /// <returns>string</returns>
        public static string GetPower(char[] PowerChars) { return new string(PowerChars); }
        /// <summary>
        /// 在权限数组中更改某个索引的权限
        /// 并返回权限数组
        /// </summary>
        /// <param name="PowerChars">PowerChars 权限数组</param>
        /// <param name="Index">当前权限数组中某个权限的索引</param>
        /// <param name="PowerValue">要更改的值</param>
        /// <returns>新的char[]权限数组</returns>
        public static char[] GetPower(char[] PowerChars, int Index, string PowerValue)
        {
            if (PowerChars == null) return null;
            if (Index < 0) return PowerChars;
            if (Index >= PowerChars.Length) return PowerChars;
            PowerChars[Index] = StringToChar(PowerValue);
            return PowerChars;
        }
        /// <summary>
        /// 在权限数组中更改某个索引的权限
        /// 并返回权限字符串
        /// </summary>
        /// <param name="PowerChars">PowerChars 权限数组</param>
        /// <param name="Index">当前权限数组中某个权限的索引</param>
        /// <param name="PowerValue">要更改的值</param>
        /// <returns>权限字符串</returns>
        public static string GetPowers(char[] PowerChars, int Index, string PowerValue)
        {
            char[] Powers = GetPower(PowerChars, Index, PowerValue);
            if (Powers == null) return "";
            return new string(Powers);
        }
        /// <summary>
        /// 在权限字符串中更改某个索引的权限
        /// 并返回权限字符串
        /// </summary>
        /// <param name="Powers">权限字符串</param>
        /// <param name="Index">当前权限数组中某个权限的索引</param>
        /// <param name="PowerValue">要更改的值</param>
        /// <returns>权限字符串</returns>
        public static string GetPowers(string Powers, int Index, string PowerValue)
        {
            if (IsNull(Powers)) return "";
            return GetPowers(Powers.ToCharArray(), Index, PowerValue);
        }
        /// <summary>
        /// 判断上一符号和当前符号 并返回罗马值
        /// 可以操作普通的加减乘除
        /// </summary>
        /// <param name="Operator1">string</param>
        /// <param name="Operator2">string</param>
        /// <returns></returns>
        private static string Precede(string Operator1, string Operator2)
        {
            switch (Operator1)
            {
                case "+":
                case "-": return ("*/(".IndexOf(Operator2) != -1) ? "<" : ">";
                case "*":
                case "/": return ((Operator2 == "(") ? "<" : ">");
                case "(": return ((Operator2 == ")") ? "=" : "<");
                case ")": return ((Operator2 == "(") ? "?" : ">");
                case "#": return ((Operator2 == "#") ? "=" : "<");
            }
            return "?";
        }
        /// <summary>
        /// 计算当前2个值的结果
        /// </summary>
        /// <param name="Operator">运算符号</param>
        /// <param name="Value1">值1</param>
        /// <param name="Value2">值2</param>
        /// <returns>Double</returns>
        private static Double Result(char Operator, Double Value1, Double Value2)
        {
            if (Operator == '+') return (Value1 + Value2);
            if (Operator == '-') return (Value1 - Value2);
            if (Operator == '*') return (Value1 * Value2);
            if (Operator == '/') return (Value1 / Value2);
            return 0;
        }
        /// <summary>
        /// VC的eval算法
        /// </summary>
        /// <param name="Expression">要计算的表达式</param>
        /// <returns>Object</returns>
        public static Object VcEval(string Expression)
        {
            //运算符的分析数组
            Stack OperatorArr = new Stack();
            //计算值的分析数组
            Stack ValueArr = new Stack();
            //要计算的表达式的长度 索引
            int i = 0;
            //相邻的要计算的2个参数
            Double Value1 = 0;
            Double Value2 = 0;
            //单字符
            string Text = "";
            //运算符
            char Operator;
            //获取运算符、计算值数组
            MatchCollection ExpArray = Regex.Matches(Expression.Replace(" ", "") + "#", @"(((?<=(^|\())-)?\d+(\.\d+)?|\D)");

            OperatorArr.Push('#');
            Text = System.Convert.ToString(ExpArray[i++]);
            while (!(Text == "#" && System.Convert.ToString(OperatorArr.Peek()) == "#"))
            {
                if ("+-*/()#".IndexOf(Text) != -1)
                {
                    switch (Precede(OperatorArr.Peek().ToString(), Text))
                    {
                        case "<":
                            OperatorArr.Push(Text);
                            Text = System.Convert.ToString(ExpArray[i++]);
                            break;
                        case "=":
                            OperatorArr.Pop();
                            Text = System.Convert.ToString(ExpArray[i++]);
                            break;
                        case ">":
                            Operator = System.Convert.ToChar(OperatorArr.Pop());
                            Value2 = System.Convert.ToDouble(ValueArr.Pop());
                            Value1 = System.Convert.ToDouble(ValueArr.Pop());
                            ValueArr.Push(Result(Operator, Value1, Value2));
                            break;
                        default:
                            return "Error";
                    }
                }
                else
                {
                    ValueArr.Push(Text);
                    Text = System.Convert.ToString(ExpArray[i++]);
                }
            }
            return ValueArr.Pop();
        }
        /// <summary>
        /// 获取随机数类型枚举对应的数字
        /// </summary>
        /// <param name="Enum">产生随机数的枚举类型</param>
        /// <returns>int</returns>
        private static int RandInt(RandEnum Enum) { return (int)Enum; }
        /// <summary>
        /// 设置随机数类型的枚举
        /// </summary>
        public enum RandEnum : int
        {
            /// <summary>
            /// 数字
            /// </summary>
            Numeric = 0,
            /// <summary>
            /// 字母
            /// </summary>
            Letter = 1,
            /// <summary>
            /// 数字字母混合
            /// </summary>
            Blend = 2,
            /// <summary>
            /// 汉字
            /// </summary>
            Chinese = 3
        }
        /// <summary>
        /// 获取全局唯一GUID 值
        /// </summary>
        /// <returns>string</returns>
        public static string GetGuid() { return GetGuid(100, true); }
        /// <summary>
        /// 获取当前时间的刻度值
        /// </summary>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>int</returns>
        public static int GetGuid(int MaxLength) { return ToInt(Right(DateTime.Now.Ticks.ToString(), 9)); }
        /// <summary>
        /// 获取全局唯一GUID 值
        /// </summary>
        /// <param name="MaxLength">最大长度</param>
        /// <param name="IsRemove">是否去掉特殊字符</param>
        /// <returns>string</returns>
        public static string GetGuid(int MaxLength, bool IsRemove)
        {
            string Guids = Guid.NewGuid().ToString();
            if (IsRemove) { Guids = Guids.Replace("{", ""); Guids = Guids.Replace("}", ""); Guids = Guids.Replace("-", ""); }
            Guids = Left(Guids, MaxLength);
            return Guids;
        }
        /// <summary>
        /// 获取指定类型的随机数
        /// </summary>
        /// <param name="Enum">产生随机数的枚举类型</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string GetGuid(RandEnum Enum, int MaxLength)
        {
            return GetGuid(Enum, "GB2312", MaxLength);
        }
        /// <summary>
        /// 获取指定类型的随机数
        /// </summary>
        /// <param name="Enum">产生随机数的枚举类型</param>
        /// <param name="Encode">产生随机中文的编码</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string GetGuid(RandEnum Enum, string Encode, int MaxLength)
        {
            if (Enum == RandEnum.Chinese) return GetGuid(Encode, MaxLength);
            string Guids = "";
            for (int i = 1; i <= MaxLength; i++)
            {
                int MinInt, MaxInt, Num;
                Random Rand = new Random(GetGuid(9) + i * 1000);
                Num = (Enum == RandEnum.Blend) ? ((Rand.Next(0, 10) <= 4) ? (int)RandEnum.Numeric : (int)RandEnum.Letter) : (int)Enum;
                MinInt = Num == 0 ? 48 : 97;
                MaxInt = Num == 0 ? 57 : 122;
                Guids += IntToString(Rand.Next(MinInt, MaxInt));
                Rand = null;
            }
            return Guids;
        }
        /// <summary>
        /// 获取随机产生的中文
        /// </summary>
        /// <param name="Encode">产生随机中文的编码</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns>string</returns>
        public static string GetGuid(string Encode, int MaxLength)
        {
            //定义返回的字符串
            string Chinese = "";
            //定义中文编码
            Encoding Ecode = Encoding.GetEncoding(Encode);
            Random Rnd = null;// new Random(GetIntRand(4));
            //int Rint = Rnd.Next(1, 100);
            //定义位码、区码的范围数
            int Wint, Qint;
            for (int i = 1; i <= MaxLength; i++)
            {
                int Rint = 0;
                //获取汉字区位
                Rnd = new Random(GetGuid(9) + i * 1000);
                Rint = Rnd.Next(16, 56);//只获取常用汉字 16-55之间
                Wint = Rint;
                //55区只89个汉字 其他 94个汉字
                Rint = (Wint == 55) ? 90 : 95;
                //定义新种子
                Rnd = new Random(GetGuid(9) + i * 3000);
                Rint = Rnd.Next(1, Rint);
                Qint = Rint;
                //两个字节变量存储产生的随机汉字区位码
                byte Fbyte = System.Convert.ToByte((Wint + 160).ToString("x"), 16);
                byte Lbyte = System.Convert.ToByte((Qint + 160).ToString("x"), 16);
                //将两个字节变量存储在字节数组中
                byte[] Nbytes = new byte[] { Fbyte, Lbyte };
                Chinese += Ecode.GetString(Nbytes);
                Rnd = null;
            }
            Ecode = null;
            return Chinese;
        }
        /// <summary>
        /// 是否汉字
        /// </summary>
        /// <param name="Object">单个字符</param>
        /// <returns>bool</returns>
        private static bool IsChinese(string Object)
        {
            int Int = StringToInt(Object);
            if (Int >= 19968 && Int <= 40869) return true;
            return false;
        }
        /// <summary>
        /// 是否汉字
        /// </summary>
        /// <param name="Object">单个字符 char</param>
        /// <returns>bool</returns>
        private static bool IsChinese(char Object)
        {
            int Int = (int)Object;
            if (Int >= 19968 && Int <= 40869) return true;
            return false;
        }
        /// <summary>
        /// 获取字符串的长度
        /// </summary>
        /// <param name="Object">要检测的对象</param>
        /// <param name="IsChecked">是否判断汉字</param>
        /// <returns>int</returns>
        public static int GetLength(string Object, bool IsChecked)
        {
            if (IsNull(Object)) return 0;
            if (!IsChecked) return Object.Length;
            int iLen = 0;
            foreach (char c in Object)
            {
                iLen++;
                if (IsChinese(c)) iLen++;
            }
            return iLen;
        }

        /// <summary>
        /// 获取字符串的长度
        /// </summary>
        /// <param name="Object">要检测的对象</param>
        /// <param name="IsChecked">是否判断汉字</param>
        /// <param name="IsHtml">是否去除HTML标签</param>
        /// <returns></returns>
        public static int GetLength(string Object, bool IsChecked, bool IsHtml)
        {
            if (IsHtml)
            {//以前的匹配写的有问题
                //string temp = Regex.Replace(Object, "/(^\\s+)|\\s+|\\n+|\\r+|(\\&nbsp;)+|<.+?>|(\\s+$)/g", ""); ;// Regex.Replace(Object, "<[^>]*>", "");
                //Object = temp.Replace(" ", "");
                Object = GetPlainText(Object);
            }

            return GetLength(Object, IsChecked);

        }

        /// <summary>
        /// 去除html 标记
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>返回字符串</returns>
        public static string GetPlainText(string text)
        {
            string temp = Regex.Replace(text, "/(^\\s+)|\\s+|\\n+|\\r+|(\\&nbsp;)+|<.+?>|(\\s+$)/g", "");
            text = temp.Replace(" ", "");
            return text;
        }
        /// <summary>
        /// 检查一段html片段是否仅包含一串按顺序排列的字符串
        /// </summary>
        /// <param name="text">检查的字符</param>
        /// <param name="checkWord">默认的字符</param>
        /// <param name="includeImg">图片是否算内容</param>
        /// <returns></returns>
        public static bool OnlyThisWord(string text, string checkWord, bool includeImg)
        {
            if (includeImg && Regex.IsMatch(text, "<img", RegexOptions.IgnoreCase))
            {
                return false;
            }
            string temp = Regex.Replace(text, "<[^>]*>", "");
            temp = Regex.Replace(temp, checkWord, "", RegexOptions.IgnoreCase);
            return IsNull(temp);
        }
        /// <summary>
        /// 检查一段html片段是否仅包含一串按顺序排列的字符串
        /// </summary>
        /// <param name="text">检查的字符</param>
        /// <param name="checkWord">图片是否算内容</param>
        /// <returns></returns>
        public static bool OnlyThisWord(string text, string checkWord)
        {
            return OnlyThisWord(text, checkWord, true);
        }

        /// <summary>
        /// 检测长度
        /// </summary>
        /// <param name="Object">要转换的对象</param>
        /// <param name="MinInt">小于等于 最小长度</param>
        /// <param name="MaxInt">大于等于 最大长度</param>
        /// <returns>符合?true:false</returns>
        public static bool CheckLength(string Object, int MinInt, int MaxInt) { return CheckLength(Object, MinInt, MaxInt, false); }
        /// <summary>
        /// 检测长度
        /// </summary>
        /// <param name="Object">要检测的对象</param>
        /// <param name="MinInt">小于等于 最小长度</param>
        /// <param name="MaxInt">大于等于 最大长度</param>
        /// <param name="IsChecked">是否判断汉字</param>
        /// <returns>符合?true:false</returns>
        public static bool CheckLength(string Object, int MinInt, int MaxInt, bool IsChecked)
        {
            int Int = GetLength(Object, IsChecked);
            if (Int < MinInt || Int > MaxInt) return false;
            return true;
        }
        /// <summary>
        /// 获取当前字符串 所对应的规定的模的值
        /// </summary>
        /// <param name="Object">当前字符串</param>
        /// <param name="UseIndex">规定的字符</param>
        /// <param name="ModInt">规定的取模数</param>
        /// <returns>模值</returns>
        public static int GetModValue(string Object, int UseIndex, int ModInt)
        {
            Object = Mid(Object, UseIndex - 1, 1);
            int Int = StringToInt(Object.ToLower());
            return Int % ModInt;
        }
        /// <summary>
        /// 获取当前单个字符 所对应的规定的模的值
        /// 字母占26个数据表
        /// 其他字符(包含汉字)占24个数据表
        /// 一共50个用户总表
        /// </summary>
        /// <param name="Object">单个字符</param>
        /// <returns>int</returns>
        public static int GetMod(string Object)
        {
            //测试为1
            //return 1;
            //
            if (Object.Length > 1) Object = Left(Object, 1);
            int Int = StringToInt(Object.ToLower());
            //0~9
            if (Int >= 48 && Int <= 57) return (1 + Int % 48);
            //a~z
            if (Int >= 97 && Int <= 122) return (11 + Int % 97);
            //其他
            return 37 + Int % 14;
        }
        /// <summary>
        /// 获取2个时间之间的时间差
        /// </summary>
        /// <param name="StarTime">第一个时间</param>
        /// <param name="EndTime">第二个时间</param>
        /// <returns>double</returns>
        public static double DateDiff(DateTime StarTime, DateTime EndTime)
        {
            try
            {
                TimeSpan StarTimeSpan = new TimeSpan(StarTime.Ticks);
                TimeSpan EndTimeSpan = new TimeSpan(EndTime.Ticks);
                TimeSpan TotalTimeSpan = StarTimeSpan.Subtract(EndTimeSpan).Duration();
                return TotalTimeSpan.TotalMilliseconds;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 计算一个时间与当前本地日期和时间的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="StarTime">一个日期和时间</param>
        /// <returns>double</returns>
        private static double DateDiff(DateTime StarTime) { return DateDiff(StarTime, DateTime.Now); }


        /// <summary>
        /// 循环补足某相同字符串
        /// </summary>
        /// <param name="Star">开始的执行数目</param>
        /// <param name="End">结束的执行数目</param>
        /// <param name="Append">要补足的某相同字符串</param>
        /// <returns>string</returns>
        public static string Append(int Star, int End, string Append)
        {
            string Return = "";
            for (int i = Star; i < End; i++) Return += Append;
            return Return;
        }
        /// <summary>
        /// 获取某日期所在其周的周一日期
        /// </summary>
        /// <param name="SomeDate">该周中任意一天</param>
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime GetMondayDate(DateTime SomeDate)
        {
            int i = SomeDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6; // i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return SomeDate.Subtract(ts);

        }

        /// <summary>
        /// 获取某日期所在其周的周日日期
        /// </summary>
        /// <param name="SomeDate">该周中任意一天</param>
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime GetSundayDate(DateTime SomeDate)
        {
            int i = SomeDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return SomeDate.Add(ts);
        }


        /// <summary>
        /// 时间换算：小时
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="Hours">小时数</param>
        /// <returns>换算后的小时数</returns>
        public static int ToHours(int Days, int Hours)
        {
            return 24 * Days + Hours;
        }
        /// <summary>
        /// 时间换算：分钟
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="Hours">小时数</param>
        /// <param name="Minutes">分钟数</param>
        /// <returns>换算后的分钟数</returns>
        public static int ToMinutes(int Days, int Hours, int Minutes)
        {
            return ToHours(Days, Hours) * 60 + Minutes;
        }
        /// <summary>
        /// 时间换算：秒
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="Hours">小时数</param>
        /// <param name="Minutes">分钟数</param>
        /// <param name="Seconds">秒数</param>
        /// <returns>换算后的秒数</returns>
        public static int ToSeconds(int Days, int Hours, int Minutes, int Seconds)
        {
            return ToMinutes(Days, Hours, Minutes) * 60 + Seconds;
        }
        /// <summary>
        /// 时间换算：小时
        /// </summary>
        /// <param name="Days">天数</param>
        /// <returns>换算后的小时数</returns>
        public static int ToHours(int Days)
        {
            return 24 * Days;
        }
        /// <summary>
        /// 时间换算：分钟
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="Hours">小时数</param>
        /// <returns>换算后的分钟数</returns>
        public static int ToMinutes(int Days, int Hours)
        {
            return (ToHours(Days) + Hours) * 60;
        }





        /// <summary>
        /// 返回时间比较差
        /// </summary>
        /// <param name="d1">时间1</param>
        /// <param name="d2">时间2</param>
        /// <param name="Parameters">参数 0 返回天数差，1小时，2分钟，3秒</param>
        /// <returns>字符串</returns>
        public static string DateDiff(DateTime d1, DateTime d2, int Parameters)
        {

            string dateDiff = null;
            try
            {

                TimeSpan ts = d1 - d2;
                switch (Parameters)
                {
                    //天
                    case 0:
                        dateDiff = ts.Days.ToString();
                        break;
                    //时
                    case 1:
                        dateDiff = (Bases.ToInt(DateDiff(d1, d2, 0), 0, -99999999, 99999999) * 24 + ts.Hours).ToString();
                        break;
                    //分
                    case 2:

                        dateDiff = (Bases.ToInt(DateDiff(d1, d2, 1), 0, -99999999, 99999999) * 60 + ts.Minutes).ToString();
                        break;
                    //秒
                    case 3:
                        dateDiff = (Bases.ToInt(DateDiff(d1, d2, 2), 0, -99999999, 99999999) * 60 + ts.Seconds).ToString();
                        break;
                }

            }
            catch
            {

            }
            return dateDiff;


        }

        /// <summary>
        /// 时间换算：秒
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="Hours">小时数</param>
        /// <param name="Minutes">分钟数</param>
        /// <returns>换算后的秒数</returns>
        public static int ToSeconds(int Days, int Hours, int Minutes)
        {
            return (ToMinutes(Days, Hours) + Minutes) * 60;
        }



        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            return HtmlEncode(theString, true);
        }
        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <param name="flag">是否将回车换行替换成br</param>
        /// <returns></returns>
        public static string HtmlEncode(string theString, bool flag)
        {
            if (IsNull(theString)) return "";
            StringBuilder sb = new StringBuilder(theString);
            sb = sb.Replace(">", "&gt;");
            sb = sb.Replace("<", "&lt;");
            sb = sb.Replace(" ", "&nbsp;");
            //sb = sb.Replace(" ", "&nbsp;");
            sb = sb.Replace("\"", "&quot;");
            sb = sb.Replace("'", "&#039");
            if (flag)
            {
                sb = sb.Replace("\n", "<br/> ");
                sb = sb.Replace("\r", "");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDiscode(string theString)
        {
            if (IsNull(theString)) return "";
            StringBuilder sb = new StringBuilder(theString);

            sb = sb.Replace("&gt;", ">");
            sb = sb.Replace("&lt;", "<");
            sb = sb.Replace("&nbsp;", " ");
            sb = sb.Replace("&nbsp;", " ");
            sb = sb.Replace("&quot;", "\"");
            sb = sb.Replace("'", "\'");
            sb = sb.Replace("&#39;", "\'");
            sb = sb.Replace("<br/> ", "\n");
            return sb.ToString();
        }
        /// <summary>
        /// 替换脚本
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FilterScript(string content)
        {
            string regexstr = @"<script[^>]*>([\s\S](?!<script))*?</script>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);

        }
        /// <summary>
        /// Textarea 页面显示
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string TextareaDiscode(string theString)
        {
            return TextareaDiscode(theString, true);
        }
        /// <summary>
        /// Textarea 页面显示
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="flag">是否更换\n\r为br</param>
        /// <returns></returns>
        public static string TextareaDiscode(string theString, bool flag)
        {
            return TextareaDiscode(theString, flag, true).ToString();
        }
        /// <summary>
        /// Textarea 页面显示
        /// </summary>
        /// <param name="theString">需要显示的string</param>
        /// <param name="istoBr">是否更换\n\r为br</param>
        /// <param name="isflag">是否将" 转成 " 转成 \"  </param>
        /// <returns>string</returns>
        public static string TextareaDiscode(string theString, bool istoBr, bool isflag)
        {
            if (IsNull(theString)) return "";
            StringBuilder sb = new StringBuilder(theString);

            if (istoBr)
            {
                sb = sb.Replace("\n", "<br/> ");
            }
            else
            {
                sb = sb.Replace("\n", " ");
            }

            if (isflag)
            {
                sb = sb.Replace("\r", "");
                sb = sb.Replace("&#39;", "\'");
                sb = sb.Replace("\"", "\\\"");
            }
            //theString = theString.Replace("{@}", "'@@@");
            return sb.ToString();
        }
        /// <summary>
        /// Html代码进行编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TextareaHtmlEncode(string source)
        {
            return TextareaDiscode(HtmlEncode(source));
        }

        /// <summary>
        /// 获取某月的第一天
        /// </summary>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime dt)
        {
            return dt.AddDays(1 - dt.Day);
        }

        /// <summary>
        /// 获取某月的最后一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime dt)
        {
            return dt.AddDays(1 - dt.Day).AddMonths(1).AddDays(-1);
        }



        /// <summary>
        /// 按字节长度截取字符串(支持截取带HTML代码样式的字符串)
        /// </summary>
        /// <param name="param">将要截取的字符串参数</param>
        /// <param name="length">截取的字节长度</param>
        /// <param name="end">字符串截取后末尾补上的字符串</param>
        /// <returns>返回截取后的字符串</returns>
        public static string subStringHTML(string param, int length, string end)
        {
            string Pattern = null;
            MatchCollection m = null;
            StringBuilder result = new StringBuilder();
            int n = 0;
            char temp;
            bool isCode = false; //是不是HTML代码
            bool isHTML = false; //是不是HTML特殊字符,如&nbsp;
            char[] pchar = param.ToCharArray();
            for (int i = 0; i < pchar.Length; i++)
            {
                temp = pchar[i];
                if (temp == '<')
                {
                    isCode = true;
                }
                else if (temp == '&')
                {
                    isHTML = true;
                }
                else if (temp == '>' && isCode)
                {
                    n = n - 1;
                    isCode = false;
                }
                else if (temp == ';' && isHTML)
                {
                    isHTML = false;
                }
                if (!isCode && !isHTML)
                {
                    n = n + 1;
                    //UNICODE码字符占两个字节
                    if (System.Text.Encoding.Default.GetBytes(temp + "").Length > 1)
                    {
                        n = n + 1;
                    }
                }
                result.Append(temp);
                if (n >= length)
                {
                    break;
                }
            }
            if (n >= length)
                result.Append(end);
            //取出截取字符串中的HTML标记
            string temp_result = result.ToString().Replace("(>)[^<>]*(<?)", "$1$2");
            //去掉不需要结素标记的HTML标记
            temp_result = temp_result.Replace(@"</?(AREA|BASE|BASEFONT|BODY|BR|COL|COLGROUP|DD|DT|FRAME|HEAD|HR|HTML|IMG|INPUT|ISINDEX|LI|LINK|META|OPTION|P|PARAM|TBODY|TD|TFOOT|TH|THEAD|TR|area|base|basefont|body|br|col|colgroup|dd|dt|frame|head|hr|html|img|input|isindex|li|link|meta|option|p|param|tbody|td|tfoot|th|thead|tr)[^<>]*/?>",
             "");
            //去掉成对的HTML标记
            temp_result = temp_result.Replace(@"<([a-zA-Z]+)[^<>]*>(.*?)</\1>", "$2");
            //用正则表达式取出标记
            Pattern = ("<([a-zA-Z]+)[^<>]*>");
            m = Regex.Matches(temp_result, Pattern);

            ArrayList endHTML = new ArrayList();

            foreach (Match mt in m)
            {
                endHTML.Add(mt.Result("$1"));
            }
            //补全不成对的HTML标记
            for (int i = endHTML.Count - 1; i >= 0; i--)
            {
                result.Append("</");
                result.Append(endHTML[i]);
                result.Append(">");
            }

            return result.ToString();
        }
        /// <summary>
        /// 提取html文本中的文字 FY
        /// </summary>
        /// <param name="strHtml">源字符串</param>
        /// <returns>文字</returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={
                                @"<script[^>]*?>.*?</script>",
                                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""''])(\\[""''tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                @"([\r\n])[\s]+",
                                @"&(quot|#34);",
                                @"&(amp|#38);",
                                @"&(lt|#60);",
                                @"&(gt|#62);",
                                @"&(nbsp|#160);",
                                @"&(iexcl|#161);",
                                @"&(cent|#162);",
                                @"&(pound|#163);",
                                @"&(copy|#169);",
                                @"&#(\d+);",
                                @"-->",
                                @"<!--.*\n"
                                };
            string[] aryRep = {
                                "",
                                "",
                                "",
                                "\"",
                                "&",
                                "<",
                                ">",
                                " ",
                                "\xa1",//chr(161), 
                                "\xa2",//chr(162), 
                                "\xa3",//chr(163), 
                                "\xa9",//chr(169), 
                                "",
                                "\r\n",
                                ""
                                };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }
        /// <summary>
        /// 截取字符串  用于特殊信息的保护
        /// </summary>
        /// <param name="beginNum"></param>
        /// <param name="endNum"></param>
        /// <param name="dealStr"></param>
        /// <returns></returns>
        public static string DealSafeStr(int beginNum, int endNum, string dealStr)
        {
            string _flag = "";    //返回标识
            if (dealStr.Length <= (beginNum + endNum))
            {
                return dealStr;
            }
            string _beginStr = ""; //开头截取的字符
            string _endStr = ""; //结尾截取的字符
            string _replaceStr = "";//替换的字符
            string _afterReplaceStr = "";//替换后的字符
            _replaceStr = dealStr.Substring(beginNum, dealStr.Length - beginNum - 1);
            _beginStr = dealStr.Substring(0, beginNum);
            _endStr = dealStr.Substring(dealStr.Length - endNum, endNum);
            Char[] cc = _replaceStr.ToCharArray();
            int intLen = _replaceStr.Length;
            for (int i = 0; i < dealStr.Length - beginNum - endNum; i++)
            {
                _afterReplaceStr = _afterReplaceStr + "*";
            }
            _flag = _beginStr + _afterReplaceStr + _endStr;
            return _flag;
        }

       


        /// <summary>
        /// 远程访问图片或者文件是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static int GetUrlError(string url)
        {
            int num = 200;
            if (url.Length > 1)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                ServicePointManager.Expect100Continue = false;
                try
                {
                    ((HttpWebResponse)request.GetResponse()).Close();
                }
                catch (WebException exception)
                {
                    if (exception.Status != WebExceptionStatus.ProtocolError)
                    {
                        return num;
                    }
                    if (exception.Message.IndexOf("500 ", StringComparison.Ordinal) > 0)
                    {
                        return 500;
                    }
                    if (exception.Message.IndexOf("401 ", StringComparison.Ordinal) > 0)
                    {
                        return 401;
                    }
                    if (exception.Message.IndexOf("404", StringComparison.Ordinal) > 0)
                    {
                        num = 404;
                    }
                }
            }
            return num;
        }

    };
}