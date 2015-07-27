using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Common.Enumspace;
using Complex.Common.Encryption;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Admin/User/
        [Dependency("RUser")]
        public IUser iUser { get; set; }

        [Dependency("RDepartment")]
        public IDepartment iDepartment { get; set; }

        [Dependency("RRole")]
        public IRole iRole { get; set; }

        [Dependency("RUserRoles")]
        public IUserRoles iUserRoles { get; set; }

        public ActionResult Index()
        {
            ViewData["Btustr"] = Btustr;
            return View();
        }
        [HttpPost]
        public JsonResult Add(T_User model)
        {
            bool Success = false;
            string Message = "添加失败请联系管理员！";
            model.Password = EncryptionMD5.GetMd5Hash(model.Password);
            int State = iUser.Add(model);
            if ( State > 0 )
            {
                Success = true;
                Message = "成功";
            }
            else if ( (CRUDState) State == CRUDState.UniqueErro )
            {
                Message = "用户名已存在,不能重复添加！";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Update(T_User model)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            model.Password = EncryptionMD5.GetMd5Hash(model.Password);
            int result = iUser.Edit(model);
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
            if (iUser.Remove(ID) > 0)
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
 
        public JsonResult GetAllUser(int page, int rows, string sort = "ID", string order = "desc", string filter = "")
        {
            //var filterArr = filter.Split(',');
            //var filterList = filterArr.Select(n => int.Parse(n)).ToArray();
            //var quer = iUser.GetAllUser();
            //int total = quer.Count();
            int total;
            var quer = iUser.GetPage(filter, page, rows, sort, order, out total);
            return Json(new { rows = quer, total = total }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取所有角色
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllRole()
        {
            var quer = iRole.GetAllRole();
            return Json(quer, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoleByUserID(int UserID)
        {
            var ownedRoles = iUserRoles.GetRoleByUserID(UserID);
            var allRoles = iRole.GetAllRole();
            var quer = (from a in ownedRoles join b in allRoles on a.RoleID equals b.ID select b).ToList();
            return Json(quer, JsonRequestBehavior.AllowGet);
        }

        public int SetRole(int UserID, string roleids)
        {
            //iUserRoles.DeleteRoleByUserID(UserID);
            if (string.IsNullOrEmpty(roleids))
            {
                return 1;
            }
            var roleIdArr = roleids.Split(',');
            var roleIdList = roleIdArr.Select(n => int.Parse(n)).ToArray();
            return iUserRoles.AddUserTo(UserID, roleIdList);
        }
        [HttpPost]
        public JsonResult SetAdmin(int UserID, string val)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            var model = iUser.GetUserByKey(UserID);
            if (model != null)
            {
                bool isamdin = val != "true";
                model.IsAdmin = isamdin;
                if (iUser.Edit(model) > 0)
                {
                    Success = true;
                    Message = "成功";
                }
            }
            return Json(new { Success = Success, Message = Message });
        }
        [HttpPost]
        public JsonResult SetEnabled(int UserID, string val)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            var model = iUser.GetUserByKey(UserID);
            if (model != null)
            {
                bool isamdin = val != "true";
                model.IsDisabled = isamdin;
                if (iUser.Edit(model) > 0)
                {
                    Success = true;
                    Message = "成功";
                }
            }
            return Json(new { Success = Success, Message = Message });
        }
    }
}
