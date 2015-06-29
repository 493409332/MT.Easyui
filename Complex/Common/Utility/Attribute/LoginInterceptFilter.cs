using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Complex.Common.Utility.Extensions;
using Complex.ICO_AOP.Utility.Factory;
using Complex.Logical.Admin;
namespace Complex.Common.Utility.Attribute
{

    public class LoginInterceptFilter : IActionFilter
    {
        #region IActionFilter 成员

        public IUserInfo iuserinfo
        {
            get
            {
                return (IUserInfo) DependencyUnityContainer.Current.Resolve(typeof(IUserInfo), "RUserInfo"); 
            }
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Request = filterContext.HttpContext.Request;
            var Session = filterContext.HttpContext.Session;
            var Cookies = filterContext.HttpContext.Request.Cookies;

            var Url = Request.Url.AbsolutePath;
            
            if ( !Url.StartsWith("/Admin/AdminLogin") )
            {
                if ( Cookies["user"] != null && Session["userinfo"] == null )
                {
                    Session["userinfo"] = iuserinfo.GetUserInfoByID(Cookies["user"].Values["userid"].ReferenceFromType<string, int>());
                }
                if ( Session["userinfo"] == null && Cookies["user"] == null )
                {
                    filterContext.Result = new RedirectResult("/Admin/AdminLogin", false);
                }
            }
           
        }


        //if (Session["userinfo"] != null) 
        //filterContext.HttpContext.Request.Url
    }

        #endregion

}
