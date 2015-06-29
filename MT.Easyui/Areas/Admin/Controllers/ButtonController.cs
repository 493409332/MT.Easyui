using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Common.Enumspace;
using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class ButtonController : BaseController
    {
        [Dependency("RButton")]
        public IButton IButton { get; set; }
        //
        // GET: /Admin/Button/

        public ActionResult Index()
        {
            ViewData["Btustr"] = Btustr;
            return View();
        }

        public JsonResult GetPage(int page, int rows, string sort = "Sortnum", string order = "desc", string filter = "")
        {	 
            int total;
            var quer = IButton.GetPage(filter, page, rows, sort, order, out total); 
            return Json(new { rows = quer, total = total }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(T_Button model)
        {
            bool Success = false;
            string Message = "添加失败请联系管理员！";
            int State = IButton.Add(model);
            if ( State > 0 )
            {
                Success = true;
                Message = "成功";
            }
            else if ( (CRUDState)State == CRUDState.UniqueErro )
            {
                Message = "权限代码已存在,不能重复添加！";
            }
            
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(T_Button model)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            if ( IButton.Edit(model) > 0 )
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            bool Success = false;
            string Message = "删除失败请联系管理员！";
            if ( IButton.Remove(ID) > 0 )
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}
