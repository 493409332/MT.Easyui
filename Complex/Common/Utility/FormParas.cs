using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Complex.Common.Utility.Extensions;
using Complex.Common.Utility.Reflector;

namespace Complex.Common.Utility
{
  public static  class FormParas
    {
       /// <summary>
        /// 判断当前是否是POST请求方式
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 判断当前是否是GET请求方式
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        

        #region 参数获取

        /// <summary>
        /// 检测URL查询参数或Form表单中是否存在某参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>存在 True 否则 False</returns>
        public static bool Has(string name)
        {
            return HasQuery(name) || HasForm(name);
        }
        /// <summary>
        /// 检测Form表单中是否存在某参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>存在 True 否则 False</returns>
        public static bool HasForm(string name)
        {
            return HttpContext.Current.Request[name] != null;
        }
        /// <summary>
        /// 检测URL查询参数中是否存在某参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>存在 True 否则 False</returns>
        public static bool HasQuery(string name)
        {
            return HttpContext.Current.Request.QueryString[name] != null;
        }

        /// <summary>
        /// 返回FORM表单参数值 或者 URL查询参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回  string.Empty </returns>
        public static string Get(string name)
        {
            string str = GetFormOrNull(name) ?? GetQueryOrNull(name);
            return str ?? string.Empty;
        }

        /// <summary>
        ///返回FORM表单参数值 或者 URL查询参数值 如果不存在则返回Null
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回 Null</returns>
        public static string GetOrNull(string name)
        {
            return GetFormOrNull(name) ?? GetQueryOrNull(name);
        }

        /// <summary>
        /// 获得FORM表单参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回  string.Empty; </returns>
        public static string GetForm(string name)
        {
            string str = HttpContext.Current.Request.Form[name];
            return str ?? string.Empty;
        }

        /// <summary>
        /// 返回FORM表单参数值 如果不存在则返回Null
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回 Null</returns>
        public static string GetFormOrNull(string name)
        {
            return HttpContext.Current.Request.Form[name];
        }

        /// <summary>
        /// 返回URL查询参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回  string.Empty; </returns>
        public static string GetQuery(string name)
        {
            string str = HttpContext.Current.Request.QueryString[name];
            return str ?? string.Empty;
        }

        /// <summary>
        /// 返回URL查询参数值 如果不存在则返回Null
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>空则返回 Null</returns>
        public static string GetQueryOrNull(string name)
        {
            return HttpContext.Current.Request.QueryString[name];
        }

        /// <summary>
        /// 获得Form表单值或者URL查询参数值列表
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>值数组</returns>
        public static string[] GetValues(string name)
        {
            string[] v = HttpContext.Current.Request.Form.GetValues(name);
            if (v == null || v.Length == 0)
            {
                return HttpContext.Current.Request.QueryString.GetValues(name);
            }
            return v;
        }

        /// <summary>
        /// 返回FORM表单参数值列表
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>值数组</returns>
        public static string[] GetFormValues(string name)
        {
            return HttpContext.Current.Request.Form.GetValues(name);
        }
        /// <summary>
        /// 返回URL查询参数值列表
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>值数组</returns>
        public static string[] GetQueryValues(string name)
        {
            return HttpContext.Current.Request.QueryString.GetValues(name);
        }


        /// <summary>
        /// 获得指定URL查询参数值转换为等效Int32形式 如果转换失败则返回默认值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>URL查询参数的int类型值</returns>
        public static int GetQueryInt(string name, int defaultValue)
        {
            string str = GetQueryOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToInt(defaultValue);
        }


        /// <summary>
        /// 获得指定FORM表单参数值转换为等效Int32形式 如果转换失败则返回默认值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>FORM表单参数的int类型值</returns>
        public static int GetFormInt(string name, int defaultValue)
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToInt(defaultValue);

        }
        /// <summary>
        /// 获得指定FORM表单参数值转换为等效Int32形式 如果转换失败则返回默认值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>FORM表单参数的int类型值</returns>
        public static DateTime GetFormDate(string name,DateTime defaultDate)
        {
          
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultDate;

            return str.ToDateTime(defaultDate);

        }
        /// <summary>
        /// 获得指定URL查询参数或者FORM表单参数值转换为等效Int32形式 如果转换失败则返回默认值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>URL查询参数或FORM表单参数的int类型值</returns>
        public static int GetInt(string name, int defaultValue)
        {
            string str = GetOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToInt(defaultValue);
        }

        /// <summary>
        /// 获得指定URL查询参数值转换为等效单精度形式 如果转换失败则返回默认值 
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>URL查询参数的float类型值</returns>
        public static float GetQueryFloat(string name, float defaultValue)
        {
            string str = GetQueryOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToFloat(defaultValue);
        }


        /// <summary>
        /// 获得指定FORM表单参数值转换为等效单精度形式 如果转换失败则返回默认值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>FORM表单参数的float类型值</returns>
        public static float GetFormFloat(string name, float defaultValue)
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToFloat(defaultValue);
        }

        /// <summary>
        ///获得指定URL查询参数或者FORM表单参数值转换为等效单精度形式 如果转换失败则返回默认值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>URL查询参数或FORM表单参数的float类型值</returns>
        public static float GetFloat(string name, float defaultValue)
        {
            string str = GetOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToFloat(defaultValue);
        }

        /// <summary>
        ///  返回表单参数值 或者 URL查询参数值 并将其转换提T类型 如果转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值 </param>
        /// <returns>转换成功则返回转换的值，否则返回defaultValue</returns>
        public static T Get<T>(string name, T defaultValue)
        {
            string str = GetOrNull(name);

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToType<T>(defaultValue);
        }


        /// <summary>
        /// 返回表单参数值并将其转换为T类型 如果转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值 </param>
        /// <returns>转换成功则返回转换的值，否则返回defaultValue</returns>
        public static T GetForm<T>(string name, T defaultValue)
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToType<T>(defaultValue);
        }

        /// <summary>
        /// 返回Form表单值是否在指定的范围内
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="minValue">最大值</param>
        /// <param name="maxValue">最小值</param>
        /// <returns></returns>
        public static bool FormInRange<T>(string name, T minValue, T maxValue) where T : struct,IComparable<T>
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return false;

            T? value = str.ToType((T?)null);
            return value.HasValue && value.Value.InRange(minValue, maxValue);
        }

        /// <summary>
        /// 返回Form表单值并将其转换为T类型 如果该值不在指定的范围内则返回默认值
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="minValue">最大值</param>
        /// <param name="maxValue">最小值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T FormIfNotInRange<T>(string name, T minValue, T maxValue, T defaultValue) where T : struct,IComparable<T>
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            T? value = str.ToType((T?)null);

            if (!value.HasValue)
                return defaultValue;

            return value.Value.IfNotInRange(minValue, maxValue, defaultValue);
        }

        /// <summary>
        /// 返回URL查询参数并将其转换为T类型  如果转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值 </param>
        /// <returns>转换成功则返回转换的值，否则返回defaultValue</returns>
        public static T GetQuery<T>(string name, T defaultValue)
        {
            string str = GetQueryOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToType<T>(defaultValue);
        }

        /// <summary>
        /// 返回URL查询参数值是否在指定的范围内
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="minValue">最大值</param>
        /// <param name="maxValue">最小值</param>
        /// <returns></returns>
        public static bool QueryInRange<T>(string name, T minValue, T maxValue) where T : struct,IComparable<T>
        {
            string str = GetQueryOrNull(name);
            if (string.IsNullOrEmpty(str))
                return false;

            T? value = str.ToType((T?)null);
            return value.HasValue && value.Value.InRange(minValue, maxValue);
        }

        /// <summary>
        /// 返回URL查询参数并将其转换为T类型 如果该值不在指定的范围内则返回默认值
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="minValue">最大值</param>
        /// <param name="maxValue">最小值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T QueryIfNotInRange<T>(string name, T minValue, T maxValue, T defaultValue) where T : struct,IComparable<T>
        {
            string str = GetQueryOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            T? value = str.ToType((T?)null);

            if (!value.HasValue)
                return defaultValue;

            return value.Value.IfNotInRange(minValue, maxValue, defaultValue);
        }


        /// <summary>
        /// 返回Form表单值或者URL查询参数值列表 并将其转换为T类型
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <returns>将转换成功的值用List返回 如果不包含值则返回空List</returns>
        public static List<T> GetValues<T>(string name) where T : struct
        {
            List<T> vs = GetQueryValues<T>(name);
            if (vs.Count < 1)
                return GetFormValues<T>(name);

            return vs;
        }


        /// <summary>
        /// 返回Form表单值列表 并将其转换为T类型的List 
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <returns>将转换成功的值用List返回 如果不包含值则返回空List</returns>
        public static List<T> GetFormValues<T>(string name) where T : struct
        {
            List<T> vs = new List<T>();
            string[] values = HttpContext.Current.Request.Form.GetValues(name);
            if (values == null || values.Length < 1)
            {
                return vs;
            }
            foreach (string i in values)
            {
                T? v = i.ToType((T?)null);
                if (v != null && v.HasValue)
                {
                    vs.Add(v.Value);
                }
            }
            return vs;
        }

        /// <summary>
        ///  返回URL查询参数值列表 并将其转换为T类型的List 
        /// </summary>
        /// <typeparam name="T">基元值类型或DateTime</typeparam>
        /// <param name="name">参数名称</param>
        /// <returns>将转换成功的值用List返回 如果不包含值则返回空List</returns>
        public static List<T> GetQueryValues<T>(string name) where T : struct
        {
            List<T> vs = new List<T>();
            string[] values = HttpContext.Current.Request.QueryString.GetValues(name);
            if (values == null || values.Length < 1)
            {
                return vs;
            }
            foreach (string i in values)
            {
                T? v = i.ToType((T?)null);
                if (v != null && v.HasValue)
                {
                    vs.Add(v.Value);
                }
            }
            return vs;
        }

        #region 表达式绑定
        #region 绑定表达式的值
        static void Bind<T>(Expression<Func<T>> expression, T value)
        {

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new InvalidOperationException("expression 应该是 MemberExpression");

            var exp = memberExpression.Expression;
            object instance = null;

            //如果不是静态属性或者字段
            if (exp != null)
            {
                var conste = memberExpression.Expression as ConstantExpression;
                if (conste == null)
                {
                    var mexp = memberExpression.Expression as MemberExpression;

                    if (mexp == null)
                        throw new InvalidOperationException("expression 不合法");

                    Stack<MemberExpression> expTrack = new Stack<MemberExpression>();
                    do
                    {
                        expTrack.Push(mexp);
                        mexp = mexp.Expression as MemberExpression;
                    } while (mexp != null);

                    mexp = expTrack.Peek();

                    //为空则最顶级为静态如 ()=> Class.StaticField.Property 
                    if (mexp.Expression != null)
                    {
                        conste = mexp.Expression as ConstantExpression;
                        //在表达式树中出现了非成员访问表达式 如方法调用 () => this.GetObj().Field 
                        if (conste == null)
                            throw new InvalidOperationException("expression 不合法");

                        instance = conste.Value;
                    }

                    //将表达式依次求值
                    while (expTrack.Count > 0)
                    {
                        mexp = expTrack.Pop();
                        var m = mexp.Member;

                        if (m is PropertyInfo)
                            instance = ReflectorCache.GetAccessor(((PropertyInfo)m)).GetValue(instance);
                        else
                            instance = ReflectorCache.GetAccessor(((FieldInfo)m)).GetValue(instance);
                    }
                }
                else
                {
                    // () => Instance.Property OR () => Instance.Field
                    instance = conste.Value;
                }
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo != null)
            {
                ReflectorCache.GetAccessor(propertyInfo).SetValue(instance, value);
            }
            else
            {
                ReflectorCache.GetAccessor((FieldInfo)memberExpression.Member).SetValue(instance, value);
            }

        }
        #endregion

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到URL查询参数值 如果转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name </param>
        /// <param name="name">绑定到的查询参数值的名称</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        public static void BindToQuery<T>(Expression<Func<T>> expression, string name, T defaultValue)
        {
            T value = GetQuery<T>(name, defaultValue);
            Bind(expression, value);
        }

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到URL查询参数值 如果转换失败则返回默认值 参数值的名称由字段/属性的名称决定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name </param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        public static void BindToQuery<T>(Expression<Func<T>> expression, T defaultValue)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("expression 应该是 MemberExpression");

            BindToQuery(expression, memberExpression.Member.Name, defaultValue);
        }

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到URL查询参数值 如果转换失败则返回default(T) 并且可以设置参数值的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="name">绑定到的查询参数值的名称</param>
        public static void BindToQueryName<T>(Expression<Func<T>> expression, string name)
        {
            BindToQuery(expression, name, default(T));
        }

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到URL查询参数值 如果转换失败则返回default(T) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        public static void BindToQuery<T>(Expression<Func<T>> expression)
        {
            BindToQuery(expression, default(T));
        }

        /// <summary>
        ///  将某引用类型实例的 属性/字段直接绑定到FORM表单值 如果转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="name">绑定到的FORM表单值的名称</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        public static void BindToForm<T>(Expression<Func<T>> expression, string name, T defaultValue)
        {
            T value = GetForm<T>(name, defaultValue);
            Bind(expression, value);
        }

        /// <summary>
        ///  将某引用类型实例的 属性/字段直接绑定到FORM表单值  如果转换失败则返回默认值 参数值的名称由字段/属性的名称决定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        public static void BindToForm<T>(Expression<Func<T>> expression, T defaultValue)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("expression 应该是 MemberExpression");

            BindToForm(expression, memberExpression.Member.Name, defaultValue);
        }

        /// <summary>
        ///  将某引用类型实例的 属性/字段直接绑定到FORM表单值 如果转换失败则返回default(T) 并且可以设置参数值的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="name">绑定到的查询参数值的名称</param>
        public static void BindToFormName<T>(Expression<Func<T>> expression, string name)
        {
            BindToForm(expression, name, default(T));
        }

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到FORM表单值 如果转换失败则返回default(T)  参数值的名称由字段/属性的名称决定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        public static void BindToForm<T>(Expression<Func<T>> expression)
        {
            BindToForm(expression, default(T));
        }

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到FORM表单值或者URL查询参数值 如果转换失败则返回默认值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="name">绑定到的FORM表单/URL查询参数值的名称</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        public static void BindTo<T>(Expression<Func<T>> expression, string name, T defaultValue)
        {
            T value = Get<T>(name, defaultValue);
            Bind(expression, value);
        }

        /// <summary>
        ///  将某引用类型实例的 属性/字段直接绑定到FORM表单值或者URL查询参数值  如果转换失败则返回默认值 参数值的名称由字段/属性的名称决定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        public static void BindTo<T>(Expression<Func<T>> expression, T defaultValue)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("expression 应该是 MemberExpression");

            BindTo(expression, memberExpression.Member.Name, defaultValue);
        }

        /// <summary>
        ///   将某引用类型实例的  属性/字段直接绑定到FORM表单值或者URL查询参数值 如果转换失败则返回default(T) 并且可以设置参数值的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        /// <param name="name">绑定到的FORM表单/URL查询参数值的名称</param>
        public static void BindToName<T>(Expression<Func<T>> expression, string name)
        {
            BindTo(expression, name, default(T));
        }

        /// <summary>
        /// 将某引用类型实例的 属性/字段直接绑定到FORM表单值或者URL查询参数值 如果转换失败则返回default(T)   参数值的名称由字段/属性的名称决定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">如 ()=>Instance.Name</param>
        public static void BindTo<T>(Expression<Func<T>> expression)
        {
            BindTo(expression, default(T));
        }
        #endregion
        #endregion

    }
}
