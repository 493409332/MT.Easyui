using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class NavigationController : BaseController
    {
        [Dependency("RNavigation")]
        public INavigation INavigation { get; set; }

        [Dependency("RButton")]
        public IButton IButton { get; set; }

        [Dependency("RNavButtons")]
        public INavButtons INavButtons { get; set; }
        // GET: /Admin/Navigation/

        public ActionResult Index()
        {
            ViewData["Btustr"] = Btustr;
            return View();
        }
        [HttpPost]
        public JsonResult Add(T_Navigation model)
        {
            bool Success = false;
            string Message = "添加失败请联系管理员！";
            if ( INavigation.Add(model) > 0 )
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(T_Navigation model)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            if ( INavigation.Edit(model) > 0 )
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
            if ( INavigation.Remove(ID) > 0 )
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        public JsonResult GetAll()
        {
            return Json(INavigation.GetTreeGrid(0), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNavButtons(int NavID)
        {
            return Json(INavButtons.GetButByNavID(NavID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetButtonsAllList()
        {
            return Json(IButton.GetAllList().OrderByDescending(p => p.Sortnum), JsonRequestBehavior.AllowGet);
        }
        public JsonResult setButtons(int NavID, string btns)
        {
            bool Success = false;
            string Message = "分配按钮失败！";

            //foreach ( var item in btns )
            //{
            //    INavButtons.Add(new T_NavButtons() { });
            //}
            string[] btnstr = btns.Split(',');
           
            if ( INavButtons.setButtons(NavID,  Array.ConvertAll<string, int>(btnstr, s => int.Parse(s))) )
            {
                Success = true;
                Message = "成功";
            }


            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}
