using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Entity.Admin;
using Microsoft.Practices.Unity;
using Complex.Common.Utility;
namespace MT.Easyui
{
    public class BaseController : Controller
    {

        [Dependency("RUserInfo")]
        public Complex.Logical.Admin.IUserInfo IUserInfo { get; set; }


        /// <summary>
        /// 当前用户信息
        /// </summary>
        public T_UserInfo  UserInfo
        {
            get
            { 
               var userinfo= IUserInfo.GetUserInfoBySession();
               if ( userinfo==null )
               {
                  Response.Redirect("/Admin/AdminLogin");
               }
            
               return userinfo;
            }
        }

        /// <summary>
        /// 获取按钮
        /// </summary>
        public string GetBtustr()
        { 
            if ( UserInfo == null )
            {
                Response.Redirect("/Admin/AdminLogin");
            }
            var Navid =Convert.ToInt32( Request["navid"]);
            if ( Navid > 0 && UserInfo.T_Navigationls.Count > 0 )
            {
                return string.Join(" <div class='datagrid-btn-separator'></div> ", UserInfo.T_Navigationls.Where(p => p.ID == Navid).Select(p => p.ButtonHtmlList).FirstOrDefault()); 
            } 
            return "";
           
        }

        /// <summary>
        /// 获取所有按钮
        /// </summary>
        public string GetAllBtustr()
        { 
            if ( UserInfo == null )
            {
                Response.Redirect("/Admin/AdminLogin");
            }
            var Navid = Convert.ToInt32(Request["navid"]);
            if ( Navid > 0 && UserInfo.AllT_Navigationls.Count > 0 )
            {
                return string.Join(" <div class='datagrid-btn-separator'></div> ", UserInfo.AllT_Navigationls.Where(p => p.ID == Navid).Select(p => p.AllButtonHtmlList).FirstOrDefault());
            }
            return "";

        }
        /// <summary>
        /// 当前按钮
        /// </summary>
        public string Btustr
        {
            get
            { 
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                }
                if ( IsAdmin )
                {
                    return GetAllBtustr();
                }
                else
                {
                    return GetBtustr();
                }
                
            }
        }
        
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public int CurrentUserID
        {
            get
            { 
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                }
                return UserInfo.T_User.ID;
            }
        }

        /// <summary>
        /// 当前用户名
        /// </summary>
        public string Name
        {
            get
            {
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                }
                var quer= UserInfo.T_User;
                return quer.TrueName==null? quer.UserName:quer.TrueName;
            }
        }
        /// <summary>
        /// 皮肤
        /// </summary>
        public string theme 
        {
            get
            {
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                }
                var quer = UserInfo.T_User;
                UserConfig model = quer.ConfigJson.Deserialize<UserConfig>();
                return model != null && model.theme.name != null ? model.theme.name : "bootstrap";
            }
        
        }
  

     //   Scripts/easyui1.3.3/themes/bootstrap/easyui.css
        /// <summary>
        /// 菜单样式
        /// </summary>
        public string showType
        {
            get
            {
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                }
                var quer = UserInfo.T_User;
                UserConfig model = quer.ConfigJson.Deserialize<UserConfig>();
                return model != null && model.showType != null ? model.showType : "tree";
            }

        }
        /// <summary>
        /// 数据表行数
        /// </summary>
        public string gridRows
        {
            get
            {
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                }
                var quer = UserInfo.T_User;
                UserConfig model = quer.ConfigJson.Deserialize<UserConfig>(); 
                return model != null && model.gridRows != null ? model.gridRows : "20";
            }

        }
        public JavaScriptResult GetUserALLConfigJs()
        {
            return JavaScript(" var userthemes = 'Scripts/easyui1.3.3/themes/" + theme + "/easyui.css'; var usergridRows=" + gridRows + ";var pageList=[10, 20, 30,40,50, 60,70,80,90,100];");
        }
      //  UserConfig model = json.Deserialize<UserConfig>();
        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                if ( UserInfo == null )
                {
                    Response.Redirect("/Admin/AdminLogin");
                } 
                return UserInfo.T_User.IsAdmin;
            }
        }
        /// <summary>
        /// 重新基类在Action执行之前的事情
        /// </summary>
        /// <param name="filterContext">重写方法的参数</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            ////得到用户登录的信息
            //CurrentUserInfo = Session["UserInfo"] as UserInfo;

            ////判断用户是否为空
            //if (CurrentUserInfo == null)
            //{
            //    Response.Redirect("/Login/Index");
            //}
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

         
            // 当自定义显示错误 mode = On，显示友好错误页面
            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
                this.View("Error").ExecuteResult(this.ControllerContext);
            }
        }

    }
}
