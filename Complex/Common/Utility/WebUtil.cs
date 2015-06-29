using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Complex.Common.Utility.Extensions;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reflection;
using Complex.Common.Utility.Reflector;
using Utility;

namespace Complex.Common.Utility
{
    /// <summary>
    /// 提供WEB开发常用功能
    /// </summary>
    public static class WebUtil
    {

        #region 私有成员
        private static IFileStorage _defaultFileStorage = new PhysicsFileStorage();
        #endregion

        /// <summary>
        /// 使用指定的 System.Text.Encoding 将查询字符串分析成一个 System.Collections.Specialized.NameValueCollection。
        /// </summary>
        /// <param name="query">查询字符串</param>
        /// <param name="encoding">文本编码</param>
        /// <param name="queryIncludeUrl">查询字符串中是否包含url</param>
        /// <returns></returns>
        public static NameValueCollection ParseQueryString(string query, Encoding encoding, bool queryIncludeUrl)
        {
            if (queryIncludeUrl)
            {
                var index = query.IndexOf('?');

                if (index == -1)
                    query = string.Empty;
                else
                    query = query.Substring(index);
            }

            return HttpUtility.ParseQueryString(query, encoding);
        }

        /// <summary>
        /// 使用指定的 System.Text.Encoding 将查询字符串分析成一个 System.Collections.Specialized.NameValueCollection。
        /// </summary>
        /// <param name="query">查询字符串</param>
        /// <param name="encoding">文本编码</param>
        /// <returns></returns>
        public static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            return ParseQueryString(query, encoding, false);
        }

        /// <summary>
        /// 使用指定的 System.Text.Encoding 将查询字符串分析成一个 System.Collections.Specialized.NameValueCollection。
        /// </summary>
        /// <param name="query">查询字符串</param>
        /// <returns></returns>
        public static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8, false);
        }


        /// <summary>
        /// 将名称值集合转换为url查询字符串
        /// </summary>
        /// <param name="col">名称值集合</param>
        /// <param name="urlEncode">是否进行url编码</param>
        /// <param name="encoding">当进行url编码是要使用的字符编码</param>
        /// <param name="excludeKeys">要排除的名称集合</param>
        /// <returns></returns>
        public static string ToQueryString(NameValueCollection col, bool urlEncode, Encoding encoding, HashSet<string> excludeKeys)
        {
            #region 方法体
            int count = col.Count;
            if (count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                string key = col.GetKey(i);
                if ((key == null) || excludeKeys == null || !excludeKeys.Contains(key))
                {
                    string str3;
                    if (urlEncode)
                    {
                        key = WebUtil.UrlEncode(key, encoding);
                    }
                    string str2 = !string.IsNullOrEmpty(key) ? (key + "=") : string.Empty;
                    string[] list = col.GetValues(i);
                    int num3 = (list != null) ? list.Length : 0;
                    if (builder.Length > 0)
                    {
                        builder.Append('&');
                    }
                    if (num3 == 1)
                    {
                        builder.Append(str2);
                        str3 = list[0];
                        if (urlEncode)
                        {
                            str3 = WebUtil.UrlEncode(str3, encoding);
                        }
                        builder.Append(str3);
                    }
                    else if (num3 == 0)
                    {
                        builder.Append(str2);
                    }
                    else
                    {
                        for (int j = 0; j < num3; j++)
                        {
                            if (j > 0)
                            {
                                builder.Append('&');
                            }
                            builder.Append(str2);
                            str3 = list[j];
                            if (urlEncode)
                            {
                                str3 = WebUtil.UrlEncode(str3, encoding);
                            }
                            builder.Append(str3);
                        }
                    }
                }
            }
            return builder.ToString();
            #endregion
        }

        /// <summary>
        /// 将名称值集合转换为url查询字符串 当需需要编码时使用当前http上下文的响应内容编码 或者 UTF8
        /// </summary>
        /// <param name="col">名称值集合</param>
        /// <param name="excludeKeys">要排除的名称集合</param>
        /// <returns></returns>
        public static string ToQueryString(NameValueCollection col, HashSet<string> excludeKeys)
        {
            Encoding encoding = null;

            var context = HttpContext.Current;
            encoding = context == null ? Encoding.UTF8 : context.Response.ContentEncoding;

            return ToQueryString(col, true, encoding, excludeKeys);
        }

        /// <summary>
        /// 修改/添加 URL查询字符串中的某个值
        /// </summary>
        /// <param name="queryString">如a=avalue&b=bvalue</param>
        /// <param name="encoding">编码的字符串</param>
        /// <param name="urlEncode">输出结果是否编码</param>
        /// <param name="name">需要修改/添加的值的名称</param>
        /// <param name="newValues">新的值</param>
        /// <returns></returns>
        public static string ChangeQueryString(string queryString, Encoding encoding, bool urlEncode, string name, params string[] newValues)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            if (name == null)
                throw new ArgumentNullException("name");

            if (!String.IsNullOrEmpty(queryString))
            {
                if (queryString[0] == '?')
                    queryString = queryString.Substring(1);
            }
            else
            {
                queryString = string.Empty;
            }

            var nvs = HttpUtility.ParseQueryString(queryString, encoding);
            if (newValues != null)
            {
                if (newValues.Length == 1)
                {
                    nvs.Set(name, newValues[0]);
                }
                else
                {
                    nvs.Remove(name);
                    foreach (var item in newValues)
                    {
                        nvs.Add(name, item);
                    }
                }
            }
            else
            {
                nvs.Remove(name);
            }
            return ToQueryString(nvs, urlEncode, encoding, null);
        }

        /// <summary>
        /// 修改/添加 URL查询字符串中的某个值 
        /// </summary>
        /// <param name="queryString">如a=avalue&b=bvalue</param>
        /// <param name="name">需要修改/添加的值的名称</param>
        /// <param name="newValues">新的值</param>
        /// <returns></returns>
        public static string ChangeQueryString(string queryString, string name, params string[] newValues)
        {
            var context = HttpContext.Current;
            var encoding = context == null ? Encoding.UTF8 : context.Response.ContentEncoding;
            return ChangeQueryString(queryString, encoding, true, name, newValues);
        }


        #region 常用功能
        public static string BuildLinkHtml(string text, string link, string target = null)
        {
            return BuildLinkHtml(text, link, target, null);
        }

        /// <summary>
        /// 生成链接标签
        /// </summary>
        /// <param name="text"></param>
        /// <param name="link"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string BuildLinkHtml(string text, string link, string target, string className = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href=\"");
            sb.Append(link);
            sb.Append('"');

            if (!string.IsNullOrEmpty(target))
            {
                sb.Append(" target=\"");
                sb.Append(target);
                sb.Append('"');
            }

            if (!string.IsNullOrEmpty(className))
            {
                sb.Append(" class=\"");
                sb.Append(className);
                sb.Append('"');
            }

            sb.Append(">");
            sb.Append(text);
            sb.Append("</a> ");

            return sb.ToString();
        }


  
        /// <summary>
        /// 获得客户端的ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        /// <summary>
        /// 返回来路页面的地址
        /// </summary>
        /// <returns></returns>
        public static string GetUrlReferrer()
        {
            return HttpContext.Current.Request.Headers["Referer"] ?? string.Empty;
        }
        /// <summary>
        /// 返回与 Web 服务器上的指定虚拟路径相对应的物理文件路径。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            var context = HttpContext.Current;
            if (context != null)
                return context.Server.MapPath(path);

            return HostingEnvironment.MapPath(path);
        }
        /// <summary>
        /// 获得当前HTTP上下文的域名包括端口号 如：http://localhost:804
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            return GetDomain(true);
        }

        /// <summary>
        /// 获得当前HTTP上下文的域名 
        /// </summary>
        /// <param name="incPort">是否包含端口号</param>
        /// <returns></returns>
        public static string GetDomain(bool incPort)
        {
            var url = HttpContext.Current.Request.Url;

            if (incPort)
                return url.Scheme + "://" + url.Authority;

            return url.Scheme + "://" + url.Host;
        }

        /// <summary>
        /// 合并两个WEB路径
        /// </summary>
        /// <param name="path1">路径1</param>
        /// <param name="path2">路径2 可以是WEB路径也可以是WINDOWS文件目录的子路径</param>
        /// <returns></returns>
        public static string Combine(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path2))
                return path1;

            if (string.IsNullOrEmpty(path1))
                return path2;

            path2 = path2.Replace('\\', '/');
            return path1.TrimEnd('/') + "/" + path2.TrimStart('/');
        }

        #region 控件HTML



        /// <summary>
        /// 获得用户控件实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="virtualPath">用户控件的虚拟路径</param>
        /// <returns></returns>
        public static T GetUserControlInstance<T>(string virtualPath) where T : UserControl
        {
            return (T)BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(T));
        }

        /// <summary>
        /// 获得服务器控件执行后的HTML片段
        /// </summary>
        /// <param name="control">控件实例</param>
        /// <returns></returns>
        public static string GetPartial(Control control)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            control.RenderControl(writer);
            return sw.ToString();
        }

        /// <summary>
        /// 获得用户控件在指定HTTP上下文执行后的HTML片段
        /// </summary>
        /// <param name="userControl">用户控件实例</param>
        /// <param name="context">HTTP上下文</param>
        /// <returns></returns>
        public static string GetPartial(UserControl userControl, HttpContext context)
        {
            var page = new System.Web.UI.Page();
            page.Controls.Add(userControl);
            StringWriter writer = new StringWriter();
            context.Server.Execute(page, writer, true);
            return writer.ToString();
        }

        /// <summary>
        /// 获得用户控件在当前HTTP上下文执行后的HTML片段
        /// </summary>
        /// <param name="userControl">用户控件</param>
        /// <returns></returns>
        public static string GetPartial(UserControl userControl)
        {
            return GetPartial(userControl, HttpContext.Current);
        }

        /// <summary>
        /// 获得指定虚拟路径用户控件、页面在当前HTTP上下文执行后的HTML片段
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <returns></returns>
        public static string GetPartial(string partialPath)
        {
            return GetPartial(partialPath, HttpContext.Current);
        }

        /// <summary>
        /// 获得指定虚拟路径用户控件、页面在指定HTTP上下文执行后的HTML片段
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="context">HTTP上下文</param>
        /// <returns></returns>
        public static string GetPartial(string partialPath, HttpContext context)
        {
            StringWriter writer = new StringWriter();
            RenderPartial(partialPath, context, writer);
            return writer.ToString();
        }



        /// <summary>
        /// 获得指定虚拟路径用户控件、页面在当前HTTP上下文执行后的HTML片段 并且可以通过让片段实例实现 ITouchable 接口来向其传递数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="data">传递的数据</param>
        /// <returns></returns>
        public static string GetPartial(string partialPath, object data)
        {
            return GetPartial(partialPath, HttpContext.Current, data);
        }

        /// <summary>
        /// 获得指定虚拟路径用户控件、页面在当前HTTP上下文执行后的HTML片段 并且可以通过让片段实例实现 泛型ITouchable 接口来向其传递强类型的数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="data">传递的数据</param>
        /// <returns></returns>
        public static string GetPartial<T>(string partialPath, T data)
        {
            return GetPartial(partialPath, HttpContext.Current, data);
        }

        /// <summary>
        /// 获得指定虚拟路径用户控件、页面在指定HTTP上下文执行后的HTML片段 并且可以通过让片段实例实现 ITouchable 接口来向其传递数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="data">传递的数据</param>
        /// <returns></returns>
        public static string GetPartial(string partialPath, HttpContext context, object data)
        {
            StringWriter writer = new StringWriter();
            RenderPartial(partialPath, context, writer, data);
            return writer.ToString();
        }

        /// <summary>
        /// 获得指定虚拟路径用户控件、页面在指定HTTP上下文执行后的HTML片段 并且可以通过让片段实例实现 泛型ITouchable 接口来向其传递强类型的数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="data">传递的数据</param>
        /// <returns></returns>
        public static string GetPartial<T>(string partialPath, HttpContext context, T data)
        {
            StringWriter writer = new StringWriter();
            RenderPartial(partialPath, context, writer, data);
            return writer.ToString();
        }

        /// <summary>
        /// 将指定虚拟路径用户控件、页面在当前HTTP上下文执行后的HTML片段呈递到当前HTTP响应输出
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        public static void RenderPartial(string partialPath)
        {
            HttpContext context = HttpContext.Current;
            RenderPartial(partialPath, context, context.Response.Output);
        }

        /// <summary>
        /// 将指定虚拟路径用户控件、页面在当前HTTP上下文执行后的HTML片段呈递到当前HTTP响应输出 并且可以通过让片段实例实现 泛型ITouchable 接口来向其传递强类型的数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="data">传递的数据</param>
        public static void RenderPartial<T>(string partialPath, T data)
        {
            HttpContext context = HttpContext.Current;
            RenderPartial(partialPath, context, context.Response.Output, data);
        }

        /// <summary>
        /// 将指定虚拟路径用户控件、页面在当前HTTP上下文执行后的HTML片段呈递到当前HTTP响应输出 并且可以通过让片段实例实现 ITouchable 接口来向其传递数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="data">传递的数据</param>
        public static void RenderPartial(string partialPath, object data)
        {
            HttpContext context = HttpContext.Current;
            RenderPartial(partialPath, context, context.Response.Output, data);
        }


        /// <summary>
        /// 将指定虚拟路径用户控件、页面在指定HTTP上下文执行后的HTML片段呈递到指定的输出
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="output">输出写入器</param>
        public static void RenderPartial(string partialPath, HttpContext context, TextWriter output)
        {
            RenderPartial(partialPath, context, output, null);
        }

        /// <summary>
        ///将指定虚拟路径用户控件、页面在HTTP上下文执行后的HTML片段呈递到响应输出 并且可以通过让片段实例实现 ITouchable 接口来向其传递数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="output">输出写入器</param>
        /// <param name="data">传递的数据</param>
        public static void RenderPartial(string partialPath, HttpContext context, TextWriter output, object data)
        {
            //  首先取得片段类型，然后获取片段关于输出缓存的定义
            //  如果需要输出缓存返回CachePartail(由其内部区处理缓存的问题)
            //  否则直接创建片段 实例并且设置数据

            var partialType = BuildManager.GetCompiledType(partialPath);
            var cachingAttributes = partialType.GetCustomAttributes(typeof(PartialCachingAttribute), true);
            object instance;

            if (cachingAttributes.Length == 0)
            {
                instance = CreatePartailInstance(partialType);

                if (data != null && instance is ITouchable)
                    ((ITouchable)instance).Data = data;
            }
            else
            {
                instance = new CachePartail<object>(partialType, (PartialCachingAttribute)cachingAttributes[0], data).GetControl();
            }

            IHttpHandler handler = instance as IHttpHandler;
            if (handler == null)
            {
                var control = instance as Control;
                Page page = new Page();
                page.Controls.Add(control);
                handler = page;
            }
            context.Server.Execute(handler, output, true);
        }

        private static object CreatePartailInstance(Type type)
        {
            var constor = type.GetConstructor(Type.EmptyTypes);
            if (constor == null)
                throw new NullReferenceException("不存在默认构造函数:" + type.FullName);

            return ReflectorCache.GetMethodInvoker(constor).Invoke(null);   //动态构建片段实例
        }


        /// <summary>
        ///将指定虚拟路径用户控件、页面在HTTP上下文执行后的HTML片段呈递到响应输出 并且可以通过让片段实例实现 泛型ITouchable 接口来向其传递强类型的数据
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="output">输出写入器</param>
        /// <param name="data">传递的数据</param>
        public static void RenderPartial<T>(string partialPath, HttpContext context, TextWriter output, T data)
        {
            var partialType = BuildManager.GetCompiledType(partialPath);
            var cachingAttributes = partialType.GetCustomAttributes(typeof(PartialCachingAttribute), true);
            object instance;

            if (cachingAttributes.Length == 0)
            {
                instance = CreatePartailInstance(partialType);

                if (data != null && instance is ITouchable<T>)
                    ((ITouchable<T>)instance).Data = data;
            }
            else
            {
                instance = new CachePartail<T>(partialType, (PartialCachingAttribute)cachingAttributes[0], data).GetControl();
            }

            IHttpHandler handler = instance as IHttpHandler;
            if (handler == null)
            {
                var control = instance as Control;
                Page page = new Page();
                page.Controls.Add(control);
                handler = page;
            }
            context.Server.Execute(handler, output, true);
        }

        #region 内部类型

        struct CachePartail<T>
        {
            public CachePartail(Type partailType, PartialCachingAttribute pcAttribute, T touchData)
            {
                this.PartailType = partailType;
                this.CacheSetting = pcAttribute;
                this.TouchData = touchData;
            }

            public T TouchData;
            public PartialCachingAttribute CacheSetting;
            public Type PartailType;

            private Control CreateControl()
            {
                var instance = CreatePartailInstance(PartailType);

                if (TouchData != null)
                {
                    var genIns = instance as ITouchable<T>;
                    if (genIns != null)
                    {
                        genIns.Data = TouchData;
                    }
                    else
                    {
                        var norIns = instance as ITouchable;
                        if (norIns != null)
                        {
                            norIns.Data = TouchData;
                        }
                    }
                }
                return (Control)instance;
            }

            public Control GetControl()
            {
                var attr = CacheSetting;
                return new StaticPartialCachingControl(String.Empty, PartailType.FullName, attr.Duration, attr.VaryByParams, attr.VaryByControls, attr.VaryByCustom, this.CreateControl);
            }
        }

        class NoCachePartial
        {
            public NoCachePartial()
            {
            }

            public NoCachePartial(string partialPath, object data)
            {
                this.PartialPath = partialPath;
                this.Data = data;
            }

            public string PartialPath { get; set; }

            public object Data { get; set; }

            public virtual string Render(HttpContext context)
            {
                return GetPartial(this.PartialPath, context, this.Data);
            }
        }

        class NoCachePartial<T> : NoCachePartial
        {
            public NoCachePartial()
            {

            }

            public NoCachePartial(string partialPath, T data)
                : base(partialPath, null)
            {

                this.Data = data;
            }

            public new T Data { get; set; }
            public override string Render(HttpContext context)
            {
                return GetPartial(this.PartialPath, context, this.Data);
            }
        }
        #endregion

        /// <summary>
        ///将指定虚拟路径用户控件、页面在HTTP上下文执行后的HTML片段呈递到响应输出 接口来向其传递数据值 并且造成的输出永远不会被输出缓存
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        public static void RenderNoCachePartial(string partialPath)
        {
            var noCache = new NoCachePartial();
            noCache.PartialPath = partialPath;
            HttpContext.Current.Response.WriteSubstitution(noCache.Render);
        }

        /// <summary>
        ///将指定虚拟路径用户控件、页面在HTTP上下文执行后的HTML片段呈递到响应输出 可以通过让片段实例实现 ITouchable 并且造成的输出永远不会被输出缓存
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="data">传递的数据 需要注意的是如果你的第一次响应被输出缓存了 那么data将会永久保持他第一次被赋予的值 直到缓存过期</param>
        public static void RenderNoCachePartial(string partialPath, object data)
        {
            var noCache = new NoCachePartial(partialPath, data);
            HttpContext.Current.Response.WriteSubstitution(noCache.Render);
        }

        /// <summary>
        ///将指定虚拟路径用户控件、页面在HTTP上下文执行后的HTML片段呈递到响应输出 可以通过让片段实例实现 ITouchable 并且造成的输出永远不会被输出缓存
        /// </summary>
        /// <param name="partialPath">用户控件、页面文件的虚拟路径</param>
        /// <param name="data">传递的数据 需要注意的是如果你的第一次响应被输出缓存了 那么data将会永久保持他第一次被赋予的值 直到缓存过期</param>
        public static void RenderNoCachePartial<T>(string partialPath, T data)
        {
            var noCache = new NoCachePartial<T>(partialPath, data);
            HttpContext.Current.Response.WriteSubstitution(noCache.Render);
        }

        #endregion

        #region Cookie操作
        /// <summary>
        /// 将对象序列化后以Base64字符串写入cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        public static void WriteCookie(string name, object value)
        {
            Byte[] data = value.BinarySerialize();
            WriteCookie(name, Convert.ToBase64String(data));
        }

        /// <summary>
        /// 将对象序列化后以Base64字符串写入cookie 并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, string aesKey)
        {
            Byte[] data = value.BinarySerialize();
            data = SecurityUtil.AESEncrypt(data, Encoding.UTF8.GetBytes(aesKey));
            WriteCookie(name, Convert.ToBase64String(data));
        }


        /// <summary>
        /// 将对象序列化后以Base64字符串写入cookie 并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="domain">cookie的域名</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, string aesKey, string domain)
        {
            Byte[] data = value.BinarySerialize();
            data = SecurityUtil.AESEncrypt(data, Encoding.UTF8.GetBytes(aesKey));
            string strValue = Convert.ToBase64String(data);

            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = strValue;

            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;

            WriteCookie(cookie);
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie  并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expiresMinute">过期时间(分钟)</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, int expiresMinute, string aesKey)
        {
            WriteCookie(name, value, DateTime.Now.AddMinutes(expiresMinute), aesKey);
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie 并且设置过期分钟
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expiresMinute">过期时间(分钟)</param>
        public static void WriteCookie(string name, object value, int expiresMinute)
        {
            WriteCookie(name, value, DateTime.Now.AddMinutes(expiresMinute));
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie 并且设置过期时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expires">过期时间</param>
        public static void WriteCookie(string name, object value, DateTime expires)
        {
            Byte[] data = value.BinarySerialize();
            WriteCookie(name, Convert.ToBase64String(data), expires);
        }

        /// <summary>
        ///  将对象序列化后以Base64字符串写入cookie 并且设置过期时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="expires">过期时间</param>
        ///  <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, object value, DateTime expires, string aesKey)
        {
            Byte[] data = value.BinarySerialize();
            data = SecurityUtil.AESEncrypt(data, Encoding.UTF8.GetBytes(aesKey));
            WriteCookie(name, Convert.ToBase64String(data), expires);
        }

        /// <summary>
        /// 向HTTP响应写入cookie值 并且对cookie值进行AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, string aesKey)
        {
            value = value.AESEncrypt(aesKey);
            WriteCookie(name, value);
        }

        /// <summary>
        /// 将字符串写入cookie 并且将其以128位AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">要写入的对象</param>
        /// <param name="domain">cookie的域名</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, string aesKey, string domain)
        {
            value = value.AESEncrypt(aesKey);
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;

            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;

            WriteCookie(cookie);
        }

        /// <summary>
        /// 向HTTP响应写入cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public static void WriteCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            WriteCookie(cookie);
        }

        /// <summary>
        ///  向HTTP响应写入cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void WriteCookie(HttpCookie cookie)
        {
            if (cookie == null) return;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期分钟
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresMinute">过期时间(分钟)</param>
        public static void WriteCookie(string name, string value, int expiresMinute)
        {
            WriteCookie(name, value, DateTime.Now.AddMinutes(expiresMinute));
        }

        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期时间和对cookie值进行AES加密
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expiresMinute"></param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, int expiresMinute, string aesKey)
        {
            value = value.AESEncrypt(aesKey);
            WriteCookie(name, value, expiresMinute);
        }

        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期时间 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        public static void WriteCookie(string name, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = expires;
            WriteCookie(cookie);
        }


        /// <summary>
        /// 向HTTP响应写入cookie 并且设置过期时间和对cookie值进行AES加密
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        /// <param name="aesKey">进行加密的128位密钥</param>
        public static void WriteCookie(string name, string value, DateTime expires, string aesKey)
        {
            value = value.AESEncrypt(aesKey);
            WriteCookie(name, value, expires);
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveCookie(string name)
        {
            HttpCookie hk = new HttpCookie(name);
            hk.Value = string.Empty;
            hk.Expires = DateTime.Now.AddYears(-1);
            WriteCookie(hk);
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveCookie(string name, string domain)
        {
            HttpCookie hk = new HttpCookie(name);
            hk.Domain = domain;
            hk.Value = string.Empty;
            hk.Expires = DateTime.Now.AddDays(-1);
            WriteCookie(hk);
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="cookie">cookie</param>
        public static void RemoveCookie(HttpCookie cookie)
        {
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读取请求中的Cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>cookie值</returns>
        public static string ReadCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                return cookie.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 读取请求中的Base64 Cookie值并将其反序列化为指定的类型， 如果不存在该值则返回默认值
        /// </summary>
        /// <typeparam name="T">将要反序列化的目标类型</typeparam>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static T ReadCookie<T>(string name)
        {
            string cookieValue = ReadCookie(name);
            if (string.IsNullOrEmpty(cookieValue))
                throw new NullReferenceException("请确保是否包含该cookie值");

            byte[] objBytes;
            try
            {
                objBytes = Convert.FromBase64String(cookieValue);
            }
            catch
            {
                throw new FormatException("请确保cookie值是否符合base64格式");
            }
            using (MemoryStream ms = new MemoryStream(objBytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }
        }

        /// <summary>
        /// 读取请求中的Cookie值并将其用128位 AES解密 将解密后的值反序列化为指定的类型
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="aesKey">进行解密的128位密钥</param>
        /// <returns></returns>
        public static T ReadCookie<T>(string name, string aesKey)
        {
            string cookieValue = ReadCookie(name);

            if (string.IsNullOrEmpty(cookieValue))
                throw new NullReferenceException("请确保是否包含该cookie值");

            byte[] objBytes;
            try
            {
                objBytes = Convert.FromBase64String(cookieValue);
            }
            catch
            {
                throw new FormatException("请确保cookie值是否符合base64格式");
            }

            try
            {
                objBytes = SecurityUtil.AESDecrypt(objBytes, Encoding.UTF8.GetBytes(aesKey));
            }
            catch
            {
                throw new CryptographicException("解密失败");
            }

            using (MemoryStream ms = new MemoryStream(objBytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }

        }

        /// <summary>
        /// 读取请求中的Cookie值并将其用128位 AES解密 如果不存在该值或者解密失败都将返回String.Empty
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="aesKey">进行解密的128位密钥</param>
        /// <returns>如果不存在该值或者解密失败都将返回String.Empty</returns>
        public static string ReadCookie(string name, string aesKey)
        {
            string value = ReadCookie(name);

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            try
            {
                value = value.AESDecrypt(aesKey);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return value;
        }

        #endregion

        /// <summary>
        /// 返回 System.String 对象转义为Javascript脚本字符串常量之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string ToJavascriptString(string str)
        {
            return ToJavascriptString(str, false);
        }

        /// <summary>
        /// 返回 System.String 对象转义为Javascript脚本字符串常量之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="addDoubleQuotes">是否添加双引号</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string ToJavascriptString(string str, bool addDoubleQuotes)
        {
            return str.ToJavascriptString(addDoubleQuotes);
        }

        /// <summary>
        /// 返回 System.String 对象中将HTML标签删除之后的字符串
        /// </summary>
        /// <param name="str">HTML字符串</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string RemoveHtml(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.RemoveHtml();
        }

        /// <summary>
        /// 将字符串转换为 HTML 编码的字符串。
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string HtmlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.HtmlEncode();
        }

        /// <summary>
        /// 将字符串转换为 HTML 编码的字符串并将输出作为 System.IO.TextWriter 输出流返回。
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <param name="output">System.IO.TextWriter 输出流</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static void HtmlEncode(string str, TextWriter output)
        {
            str = str ?? string.Empty;
            str.HtmlEncode(output);
        }

        /// <summary>
        /// 已经为 HTTP 传输进行过 HTML 编码的字符串转换为已解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string HtmlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.HtmlDecode();
        }

        /// <summary>
        /// 将已经过 HTML 编码的字符串转换为已解码的字符串并将其发送给 System.IO.TextWriter 输出流。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <param name="output">System.IO.TextWriter 输出流</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static void HtmlDecode(string str, TextWriter output)
        {
            str = str ?? string.Empty;
            str.HtmlDecode(output);
        }

        /// <summary>
        /// 对 URL 字符串进行编码。
        /// </summary>
        /// <param name="str">要编码的文本</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.UrlEncode();
        }

        /// <summary>
        /// 使用指定的编码对象对 URL 字符串进行编码。
        /// </summary>
        /// <param name="str">要编码的文本</param>
        /// <param name="encoding">指定编码方案的 System.Text.Encoding 对象</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string UrlEncode(string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.UrlEncode(encoding);
        }

        /// <summary>
        /// 将已经为在 URL 中传输而编码的字符串转换为解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.UrlDecode();
        }

        /// <summary>
        /// 使用指定的编码对象将 URL 编码的字符串转换为已解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <param name="encoding">指定解码方法的 System.Text.Encoding</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string UrlDecode(string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return str.UrlDecode(encoding);
        }

        /// <summary>
        /// 将字符串进行HTML属性编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>如果str为null则返回 string.Empty</returns>
        public static string HtmlAttributeEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return HttpUtility.HtmlAttributeEncode(str);
        }

        /// <summary>
        /// 解码htmldecode
        /// </summary>
        /// <param name="str_web"></param>
        /// <returns></returns>
        public static string HtmlCustomDecode(string str_web)
        {

            if (string.IsNullOrEmpty(str_web))
                return string.Empty;

            //str_web = str_web.Replace("&quot;", "'");
            str_web = str_web.Replace("&lt;", "<");
            str_web = str_web.Replace("&gt;", ">");
            str_web = str_web.Replace("&quot;", "\"");
            return str_web;
        }

        /// <summary>
        /// string中的多种特定字符替换处理(如将"+","，"等替换成英文逗号，去除无效空格)
        /// </summary>
        /// <param name="str">需要处理的字符串</param>
        /// <param name="separatorchar">字符串中待替换的字符列表，如为null则默认',',' ','+'</param>
        /// <param name="targetchar">字符串中替换后的字符，如为空则默认','</param>
        /// <returns></returns>
        public static string StringSeparatorDeal(string str, List<char> separatorchar = null, char targetchar = ',')
        {
            if (string.IsNullOrEmpty(str) || (str = str.Trim()).Length < 1)
                return string.Empty;

            var separators = separatorchar == null || separatorchar.Count == 0 ? new char[] { '，', ' ', '+' } : separatorchar.ToArray();
            var strs = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(targetchar.ToString(), strs);
        }

        /// <summary>
        /// 以指定的ContentType输出指定文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType  "application/octet-stream"表示输出文件即出现下载对话框</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            byte[] buffer = new Byte[10000];

            // 文件长度
            int length;

            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // 需要读的数据长度
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string HtmlSpaces(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append("&nbsp;");
            }
            return sb.ToString();
        }

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

        #endregion

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
        public static bool GetFormBool(string name, bool defaultValue)
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToBoolean(defaultValue);

        }
        public static DateTime GetFormDate(string name, DateTime defaultValue)
        {
            string str = GetFormOrNull(name);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str.ToDateTime(defaultValue);

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

        #region 文件上传区域

        /// <summary>
        /// 通过FORM表单上传客户端文件
        /// </summary>
        /// <param name="formFileName">FORM表单文件输入域的名称</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="newfile">文件存放的路径</param>
        /// <returns>文件上传的结果 如果相应的表单文件输入域不存在则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(string formFileName, int maxSize, int minSize, string[] exts, string newfile)
        {
            return UpLoad(formFileName, newfile, maxSize, minSize, exts, _defaultFileStorage);
        }

        /// <summary>
        /// 通过FORM表单上传客户端文件 并且自动生成路径和文件名
        /// </summary>
        /// <param name="formFileName">FORM表单文件输入域的名称</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="basepath">文件存放的基本路径如：C:\UploadFile</param>
        /// <param name="newfile">返回自动生成的部分 文件存放的路径和文件名 如:2008\9\1\3\236548593.gif</param>
        /// <returns>文件上传的结果 如果相应的表单文件输入域不存在则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(string formFileName, int maxSize, int minSize, string[] exts, string basepath, out string newfile)
        {
            return UpLoad(formFileName, out newfile, maxSize, minSize, exts, basepath, _defaultFileStorage);
        }


        /// <summary>
        /// 通过HttpPostedFile上传客户端文件
        /// </summary>
        /// <param name="postedFile">HttpPostedFile对象的引用</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="newfile">文件存放的路径</param>
        /// <returns>文件上传的结果 如果postedFile为null则返回UploadResult.FileEmpty </returns>
        public static UploadResult UpLoad(HttpPostedFile postedFile, int maxSize, int minSize, string[] exts, string newfile)
        {
            return UpLoad(postedFile, newfile, maxSize, minSize, exts, _defaultFileStorage);
        }

        /// <summary>
        /// 通过HttpPostedFile上传客户端文件 并且自动生成路径和文件名 
        /// </summary>
        /// <param name="postedFile">HttpPostedFile对象的引用</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="basepath">文件存放的基本路径如：C:\UploadFile</param>
        /// <param name="newfile">返回自动生成的部分 文件存放的路径和文件名 如:2008\9\1\3\236548593.gif</param>
        /// <returns>文件上传的结果 如果postedFile为null则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(HttpPostedFile postedFile, int maxSize, int minSize, string[] exts, string basepath, out string newfile)
        {
            return UpLoad(postedFile, out newfile, maxSize, minSize, exts, basepath, _defaultFileStorage);
        }

        /// <summary>
        /// 通过FileUpload上传客户端文件 并且自动生成路径和文件名 
        /// </summary>
        /// <param name="filec">FileUpload对象的引用</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="basepath">文件存放的基本路径如：C:\UploadFile</param>
        /// <param name="newfile">返回自动生成的部分 文件存放的路径和文件名 如:2008\9\1\3\236548593.gif</param>
        /// <returns>文件上传的结果  如果filec为null则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(FileUpload filec, int maxSize, int minSize, string[] exts, string basepath, out string newfile)
        {
            return UpLoad(filec, out newfile, maxSize, minSize, exts, basepath, _defaultFileStorage);
        }

        /// <summary>
        /// 通过FileUpload上传客户端文件
        /// </summary>
        /// <param name="filec">FileUpload对象的引用</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="newfile">文件存放的路径</param>
        /// <returns>文件上传的结果  如果filec为null则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(FileUpload filec, int maxSize, int minSize, string[] exts, string newfile)
        {
            return UpLoad(filec, newfile, maxSize, minSize, exts, _defaultFileStorage);
        }


        /// <summary>
        /// 通过HtmlInputFile上传客户端文件
        /// </summary>
        /// <param name="filec">HtmlInputFile对象的引用</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="newfile">文件存放的路径</param>
        /// <returns>文件上传的结果  如果filec为null则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(HtmlInputFile filec, int maxSize, int minSize, string[] exts, string newfile)
        {
            return UpLoad(filec, newfile, maxSize, minSize, exts, _defaultFileStorage);
        }

        /// <summary>
        /// 通过HtmlInputFile上传客户端文件 并且自动生成路径和文件名
        /// </summary>
        /// <param name="filec">HtmlInputFile对象的引用</param>
        /// <param name="maxSize">文件的最大字节数 小于1则不限制</param>
        /// <param name="minSize">文件的最小字节数 小于1则不限制</param>
        /// <param name="exts">允许的文件扩展名 如:new String[]{".gif",".jpg",".png"}</param>
        /// <param name="basepath">文件存放的基本路径如：C:\UploadFile</param>
        /// <param name="newfile">返回自动生成的部分 文件存放的路径和文件名 如:2008\9\1\3\236548593.gif</param>
        /// <returns>文件上传的结果 如果filec为null则返回UploadResult.FileEmpty</returns>
        public static UploadResult UpLoad(HtmlInputFile filec, int maxSize, int minSize, string[] exts, string basepath, out string newfile)
        {
            return UpLoad(filec, out newfile, maxSize, minSize, exts, basepath, _defaultFileStorage);
        }

        #region 新增 2011-9-23

        public static UploadResult UpLoad(string formFileName, string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, IFileStorage storage = null)
        {
            if (formFileName == null)
                throw new ArgumentNullException("formFileName");

            if (newfile == null)
                throw new ArgumentNullException("newfile");

            HttpPostedFile postedFile = HttpContext.Current.Request.Files[formFileName];
            if (postedFile == null || postedFile.ContentLength < 1)
            {
                return UploadResult.FileEmpty;
            }

            return UpLoadInternal(postedFile, maxSize, minSize, exts, newfile, storage ?? _defaultFileStorage);
        }

        public static UploadResult UpLoad(string formFileName, out string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, string basepath = "", IFileStorage storage = null)
        {
            if (formFileName == null)
                throw new ArgumentNullException("formFileName");

            if (basepath == null)
                throw new ArgumentNullException("basepath");

            HttpPostedFile postedFile = HttpContext.Current.Request.Files[formFileName];
            if (postedFile == null || postedFile.ContentLength < 1)
            {
                newfile = string.Empty;
                return UploadResult.FileEmpty;
            }

            storage = storage ?? _defaultFileStorage;
            newfile = storage.MakeFileName(Path.GetExtension(postedFile.FileName));
            return UpLoadInternal(postedFile, maxSize, minSize, exts, basepath + newfile, storage);
        }

        public static UploadResult UpLoad(HttpPostedFile postedFile, string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, IFileStorage storage = null)
        {
            #region
            if (postedFile == null || postedFile.ContentLength < 1)
            {
                return UploadResult.FileEmpty;
            }

            if (newfile == null)
                throw new ArgumentNullException("newfile");

            return UpLoadInternal(postedFile, maxSize, minSize, exts, newfile, storage ?? _defaultFileStorage);
            #endregion
        }

        public static UploadResult UpLoad(HttpPostedFile postedFile, out string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, string basepath = "", IFileStorage storage = null)
        {
            if (postedFile == null || postedFile.ContentLength < 1)
            {
                newfile = string.Empty;
                return UploadResult.FileEmpty;
            }

            if (basepath == null)
                throw new ArgumentNullException("basepath");

            storage = storage ?? _defaultFileStorage;
            newfile = storage.MakeFileName(Path.GetExtension(postedFile.FileName));
            return UpLoadInternal(postedFile, maxSize, minSize, exts, basepath + newfile, storage);
        }


        public static UploadResult UpLoad(FileUpload filec, out string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, string basepath = "", IFileStorage storage = null)
        {
            #region
            HttpPostedFile postedFile;
            if (filec == null || (postedFile = filec.PostedFile) == null || postedFile.ContentLength < 1)
            {
                newfile = string.Empty;
                return UploadResult.FileEmpty;
            }

            if (basepath == null)
                throw new ArgumentNullException("basepath");

            storage = storage ?? _defaultFileStorage;
            newfile = storage.MakeFileName(Path.GetExtension(postedFile.FileName));
            return UpLoadInternal(postedFile, maxSize, minSize, exts, basepath + newfile, storage);

            #endregion
        }

        public static UploadResult UpLoad(FileUpload filec, string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, IFileStorage storage = null)
        {
            HttpPostedFile postedFile;
            if (filec == null || (postedFile = filec.PostedFile) == null || postedFile.ContentLength < 1)
            {
                return UploadResult.FileEmpty;
            }

            if (newfile == null)
                throw new ArgumentNullException("newfile");

            return UpLoadInternal(postedFile, maxSize, minSize, exts, newfile, storage ?? _defaultFileStorage);
        }



        public static UploadResult UpLoad(HtmlInputFile filec, string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, IFileStorage storage = null)
        {

            HttpPostedFile postedFile;
            if (filec == null || (postedFile = filec.PostedFile) == null || postedFile.ContentLength < 1)
            {
                return UploadResult.FileEmpty;
            }

            if (newfile == null)
                throw new ArgumentNullException("newfile");

            return UpLoadInternal(postedFile, maxSize, minSize, exts, newfile, storage ?? _defaultFileStorage);
        }

        public static UploadResult UpLoad(HtmlInputFile filec, out string newfile, int maxSize = 0, int minSize = 0, string[] exts = null, string basepath = "", IFileStorage storage = null)
        {
            HttpPostedFile postedFile;
            if (filec == null || (postedFile = filec.PostedFile) == null || postedFile.ContentLength < 1)
            {
                newfile = string.Empty;
                return UploadResult.FileEmpty;
            }

            if (basepath == null)
                throw new ArgumentNullException("basepath");

            storage = storage ?? _defaultFileStorage;
            newfile = storage.MakeFileName(Path.GetExtension(postedFile.FileName));
            return UpLoadInternal(postedFile, maxSize, minSize, exts, basepath + newfile, storage);
        }


        internal static UploadResult UpLoadInternal(HttpPostedFile postedFile, int maxSize, int minSize, string[] exts, string newfile, IFileStorage storage)
        {
            #region
            if (maxSize > 0 && postedFile.ContentLength > maxSize) return UploadResult.FileOverflow;
            if (minSize > 0 && postedFile.ContentLength < minSize) return UploadResult.FileShort;

            string ext = Path.GetExtension(postedFile.FileName);
            if (exts != null &&
                exts.Length != 0 &&
                !exts.Contains<string>(ext, StringComparer.OrdinalIgnoreCase))
            {
                return UploadResult.TypeNotAllow;
            }

            storage.Save(postedFile.InputStream, newfile);
            return UploadResult.Succed;
            #endregion
        }


        /// <summary>
        /// 注册默认的文件存储器
        /// </summary>
        /// <param name="storage"></param>
        public static void RegisterDefaultFileStorage(IFileStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _defaultFileStorage = storage;
        }

        /// <summary>
        /// 当前使用的默认文件存储器
        /// </summary>
        public static IFileStorage DefaultFileStorage
        {
            get
            {
                return _defaultFileStorage;
            }
        }
        #endregion

        #endregion
    }


}
