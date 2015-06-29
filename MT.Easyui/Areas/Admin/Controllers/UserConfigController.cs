using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Common.Utility;
using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;
namespace MT.Easyui.Areas.Admin.Controllers
{
    public class UserConfigController : BaseController
    {
        //
        // GET: /Admin/UserConfig/
        [Dependency("RUser")]
        public IUser iUser { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public JavaScriptResult GetConfig()
        {
            return  JavaScript( " var sys_config = " + iUser.GetConfig(CurrentUserID));
        }
        public int SetConfig(string json)
        {  
            return iUser.SetUseConfigrByKey(CurrentUserID, json);
        }
    }
}
