using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class AdminLogController : BaseController
    {
        [Dependency("RAdminLog")]
        public IAdminLog IAdminLog { get; set; }
        //
        // GET: /Admin/AdminLog/

        public ActionResult Index()
        {
            ViewData["UserName"] = UserInfo.T_User.UserName;
            return View();
        }
        public JsonResult GetPage(int page, int rows, string sort = "OperationTime", string order = "desc", string filter = "",string runclass="",string username="")
        {

            int total;
            var quer = IAdminLog.GetPage(filter, page, rows, sort, order, runclass, username, out total);


            return Json(new { rows = quer, total = total } , JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClasslist()
        {
            return Json(IAdminLog.GetClassList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserNamelist()
        {
            return Json(IAdminLog.GetUserNamelist(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete()
        {
            return Json(IAdminLog.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}
