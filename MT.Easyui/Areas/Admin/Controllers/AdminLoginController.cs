using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Entity.Admin;
using Complex.Logical.Admin.CacheManag;
using Microsoft.Practices.Unity;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        //
        // GET: /Admin/AdminLogin/
        [Dependency("RLogin")]
        public Complex.Logical.Admin.ILogin ILogin { get; set; }
        [Dependency("RUserInfo")]
        public Complex.Logical.Admin.IUserInfo IUserInfo { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public void ClearSessionOrCookies()
        {
            if (Session["userinfo"] is T_UserInfo)
	        { 
                CacheManagement.Instance.RemoveByID("T_UserInfo" , ( Session["userinfo"] as T_UserInfo ).T_User.ID); 
            }
            else if ( Request.Cookies["user"].Values["userid"] != null )
            {
                Complex.Common.Cache.MyCache.Instance.Remove("T_UserInfo&" + Request.Cookies["user"].Values["userid"]);
                CacheManagement.Instance.RemoveByID("T_UserInfo",int.Parse( Request.Cookies["user"].Values["userid"] )); 
            }
           
            Session["userinfo"] = null;
            if ( Request.Cookies["user"] == null )
            {
                return;
            }
            if ( Request.Cookies["user"].Values["userid"] == null )
            {
                return;
            }
          //  Complex.Common.Cache.DataCache.RemoveCache(Request.Cookies["user"].Values["userid"].ToString());
            Request.Cookies.Clear();
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["user"].Path = "/";

        }
        [HttpPost]
        public ActionResult Index(T_User usermodel)
        {

            if ( ILogin.SearchForName(usermodel.UserName) )
            {
               usermodel= ILogin.SearchForNameorPwd(usermodel.UserName,  Complex.Common.Encryption.EncryptionMD5.GetMd5Hash( usermodel.Password));
               if ( usermodel!=null )
                {
                    CacheManagement.Instance.RemoveByID("T_UserInfo", usermodel.ID);

                    Session["userinfo"] = IUserInfo.GetUserInfo(usermodel);
                    System.Web.HttpCookie newcookie = new HttpCookie("user");
                    newcookie.Values["userid"] = usermodel.ID.ToString();
                    Complex.Common.Cache.DataCache.RemoveCache(usermodel.ID.ToString());
                    newcookie.Expires = DateTime.Now.AddDays(365);
                    Response.AppendCookie(newcookie); 

                    return RedirectToRoute("Admin_default", new
                    {
                        controller = "AdminHome",
                        action = "Index"
                    });

                }
                else
                {
                    ModelState.AddModelError("Message", "密码错误！");
                }
               
            }
            else
            {
                ModelState.AddModelError("Message", "用户不存在！");
            }

            return View();
        }

    }
}
