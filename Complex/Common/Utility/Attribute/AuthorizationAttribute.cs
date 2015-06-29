using Complex.Common.Cache;
using Complex.Entity; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Complex.Common.Utility.Attribute
{
    /// <summary>
    /// 表示需要用户登录才可以使用的特性
    /// <para>如果不需要处理用户登录，则请指定AllowAnonymousAttribute属性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {

        string[] PurviewName;
        public AuthorizationAttribute(string[] purviewtype)
        {
            if ( purviewtype != null )
            {
                PurviewName = purviewtype;
            }
        }

        /// <summary>
        /// 处理用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {


            filterContext.Result = new RedirectResult("/UserLogin/Index", false);
        }
    }
}
