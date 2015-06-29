using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        //
        // GET: /Admin/Role/
        [Dependency("RRole")]
        public IRole iRole { get; set; }

        [Dependency("RButton")]
        public IButton iButton { get; set; }

        [Dependency("RRoleNavBtns")]
        public IRoleNavBtns iRoleNavBtns { get; set; }

        [Dependency("RNavigation")]
        public INavigation iNavigation { get; set; }

        [Dependency("RNavButtons")]
        public INavButtons iNavButtons { get; set; }

        public ActionResult Index()
        {
            ViewData["Btustr"] = Btustr;
            return View();
        }
        [HttpPost]
        public JsonResult Add(T_Role model)
        {
            bool Success = false;
            string Message = "添加失败请联系管理员！";
            if (iRole.Add(model) > 0)
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPage(int page, int rows, string sort = "ID", string order = "desc", string filter = "")
        {
            int total;
            var quer = iRole.GetPage(filter, page, rows, sort, order, out total);
            return Json(new { rows = quer, total = total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(T_Role model)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            if (model.IsDefault.ToString().Equals("on"))
            {
                model.IsDefault = 1;
            }
            else
            {
                model.IsDefault = 0;
            }
            int result = iRole.Edit(model);
            if (result > 0)
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
            if (iRole.Remove(ID) > 0)
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetNavigation(int roleID)
        {
            var nav = iNavigation.GetAllList().OrderByDescending(p => p.Sortnum).ToList();
            var navBtn = iNavButtons.GetAllList();
            var RolnavBtn = iRoleNavBtns.GetRoleNavBtnsByRole(roleID);
            var btn = iButton.GetButtons();
            var navBtnQuer = from a in navBtn join b in btn on a.ButtonId equals b.ID select new { NavID = a.NavId, Buttons = b.ButtonTag };
            var navOwnedBtnQuer = from a in RolnavBtn join b in btn on a.BtnID equals b.ID select new { NavID = a.NavID, Buttons = b.ButtonTag };
            var navquer = from a in nav
                          select new T_Navigation()
                          {
                              ID = a.ID,
                              NavTitle = a.NavTitle,
                              iconCls = a.iconCls,
                              Buttons = navBtnQuer.Where(p => p.NavID == a.ID).Select(p => p.Buttons).ToList(),
                              OwnedBut = navOwnedBtnQuer.Where(p => p.NavID == a.ID).Select(p => p.Buttons).ToList(),
                              BigImageUrl = a.BigImageUrl,
                              iconUrl = a.iconUrl,
                              ParentID = a.ParentID,
                              IsDelete = a.IsDelete,
                              IsSys = a.IsSys,
                              IsVisible = a.IsVisible,
                              Linkurl = a.Linkurl,
                              NavTag = a.NavTag,
                              Sortnum = a.Sortnum
                          };

            List<T_Navigation> datatreating = new List<T_Navigation>();
            if (navquer != null)
            {
                datatreating = Setchildren(navquer.ToList(), 0);
            }
            return Json(datatreating, JsonRequestBehavior.AllowGet);
        }

        public List<T_Navigation> Setchildren(List<T_Navigation> quer, int ParentID)
        {
            var datatreating = new List<T_Navigation>();

            foreach (var item in quer.Where(p => p.ParentID == ParentID).OrderByDescending(p => p.Sortnum).ToList())
            {
                item.children = Setchildren(quer, item.ID);
                datatreating.Add(item);
            }

            return datatreating;
        }

        /// <summary>
        /// 创建treegrid的所有按钮列
        /// </summary>
        /// <returns></returns>
        public JsonResult BuildNavBtnsColumns()
        {
            var list = iButton.GetButtons();
            var json = from n in list
                       orderby n.Sortnum descending
                       select new
                       {
                           title = n.ButtonText,
                           field = n.ButtonTag,
                           width = 60,
                           align = "center",
                           editor = new { type = "checkbox", options = new { @on = "√", off = "x" } }
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult setRoleButtons(string Data)
        {
            bool Success = false;
            string Message = "删除失败请联系管理员！";
            if ( iRoleNavBtns.setRoleButtons(Data)   )
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}
