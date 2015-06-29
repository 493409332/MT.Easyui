using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Entity;
using Microsoft.Practices.Unity;

namespace MT.Easyui.Controllers
{
    public class HomeController : Controller
    {
        [Dependency("RButton")]
        public Complex.Logical.Admin.IButton IButton { get; set; }
       

        public ActionResult Index()
        {
            //decimal aaa= ITest2.IsLogin(new test2(), 1, 1, new object(), 1.1f);
          return Redirect("~/Admin/AdminLogin/Index");
           // return RedirectToRoute("Index", "AdminHome", "Admin_default");
            //int aaa;
            //var quer = IButton.Add(new Complex.Entity.Admin.T_Button());
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

          //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
