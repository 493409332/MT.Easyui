using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Complex.Common.Config
{
    public class HostInfo
    {
        /// <summary>
        /// 返回来路页面的地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            return (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
                    && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty)
                       ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                       : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
