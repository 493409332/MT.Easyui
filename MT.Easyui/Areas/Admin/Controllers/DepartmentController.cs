using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Common.Utility;

namespace MT.Easyui.Areas.Admin.Controllers
{
    public class DepartmentController : BaseController
    {
        //
        // GET: /Admin/Department/
        [Dependency("RDepartment")]
        public IDepartment iDepartment { get; set; }

        public ActionResult Index()
        {
            ViewData["Btustr"] = Btustr;
            return View();
        }
        public JsonResult GetPage(int page, int rows, string sort = "ID", string order = "desc", string filter = "")
        {
            int total;
            var quer = iDepartment.GetPage(filter, page, rows, sort, order, out total);
            return Json(new { rows = quer, total = total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(T_Department model)
        {
            bool Success = false;
            string Message = "添加失败请联系管理员！";
            if (iDepartment.Add(model) > 0)
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(T_Department model)
        {
            bool Success = false;
            string Message = "修改失败请联系管理员！";
            if (iDepartment.Edit(model) > 0)
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
            if (iDepartment.Remove(ID) > 0)
            {
                Success = true;
                Message = "成功";
            }
            return Json(new { Success = Success, Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            return Json(iDepartment.GetTreeGrid(0), JsonRequestBehavior.AllowGet);
        }

        public string GetAllReplace()
        {
            List<T_Department> list = iDepartment.GetDepartment(0);
            //var quer = from a in list select new { ID = a.ID, ParentId = a.ParentId, Sortnum = a.Sortnum, Remark = a.Remark, IsDelete = a.IsDelete, children = a.children, text = a.DepartmentName };
  
            //var settings = new JsonSerializerSettings
            //{
            //    ContractResolver =new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            //}; 
            //var json = JsonConvert.SerializeObject(list, settings); 
            return list.SerializeCamelCasePropertyNamesLower();
        }
        //public ActionResult DepartmentHandler()
        //{
        //    UserBll.Instance.CheckUserOnlingState();

        //    int k;
        //    var context = System.Web.HttpContext.Current;
        //    var json = System.Web.HttpContext.Current.Request["json"];
        //    var rpm = new RequestParamModel<Department>(context) { CurrentContext = context };
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        rpm = JSONhelper.ConvertToObject<RequestParamModel<Department>>(json);
        //        rpm.CurrentContext = context;
        //    }

        //    switch (rpm.Action)
        //    {
        //        case "add":
        //            return Content(DepartmentBll.Instance.AddNewDepartment(rpm.Entity));
        //        case "edit":
        //            Department d = new Department();
        //            d.InjectFrom(rpm.Entity);
        //            d.KeyId = rpm.KeyId;
        //            return Content(DepartmentBll.Instance.EditDepartment(d));
        //        case "delete":
        //            return Content(DepartmentBll.Instance.DeleteDepartment(rpm.KeyId));
        //        default:
        //            return Content(DepartmentBll.Instance.GetDepartmentTreegridData());
        //    }
        //}
       
    }
}
