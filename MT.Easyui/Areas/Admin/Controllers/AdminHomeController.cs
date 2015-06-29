using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Complex.Logical.Admin;
using Microsoft.Practices.Unity;
using Complex.Common.Utility.Extensions;
 
namespace MT.Easyui.Areas.Admin.Controllers
{
    public class AdminHomeController : BaseController
    {
        //{
        //            "id": 2, "text": "操作按钮", "iconCls": "icon-bricks", "attributes": { "url": "/Admin/Button/Index", "iconUrl": "/Content/iconcss/icon/bricks.png", "parentid": 1, "sortnum": 1, "BigImageUrl": "/Content/iconcss/icon/32/bricks.png" }
        //            , "children": [{ "id": 30, "text": "添加", "iconCls": "icon-add", "attributes": { "url": "2222", "iconUrl": "/Content/iconcss/icon/16/add.png", "parentid": 2, "sortnum": 1, "BigImageUrl": "/Content/iconcss/icon/32/add.png" }, "children": [] }

        //                , { "id": 32, "text": "修改", "iconCls": "icon-edit", "attributes": { "url": "/", "iconUrl": "/Content/iconcss/icon/16/edit.gif", "parentid": 2, "sortnum": 1, "BigImageUrl": "/Content/iconcss/icon/32/page_edit.png" }, "children": [] }, { "id": 33, "text": "删除", "iconCls": "icon-delete", "attributes": { "url": "/", "iconUrl": "/Content/iconcss/icon/16/delete.png", "parentid": 2, "sortnum": 3, "BigImageUrl": "/Content/iconcss/icon/32/cross.png" }, "children": [] }]
        //        }, 
        //
        // GET: /Admin/Home/
        [Dependency("RNavigation")]
        public INavigation INavigation { get; set; }

        public ActionResult Index()
        {
            ViewData["Name"] = Name;

            ViewData["theme"] = theme;
            ViewData["showType"] = showType;
           // ViewData["gridRows"] = gridRows;
            
        //    ViewData["Navigationls"] =
          //var Navigationlstem=   UserInfo.T_Navigationls.Select(p => new T_UserInfoTem(new List<T_UserInfoTem>()) { id = p.ID, text = p.NavTitle, iconCls = p.iconCls, attributes = new attributes{ url = p.Linkurl, iconUrl = p.iconUrl, parentid = p.ParentID, sortnum = p.Sortnum, BigImageUrl = p.BigImageUrl }  });

          // var Navigationls = new List<T_UserInfoTem>(Navigationlstem);
          // Navigationls.AddRange(Navigationlstem);


            if ( IsAdmin )
            {
                ViewData["Navigation"] = INavigation.GetNavigation().JSONSerialize();
            }
            else
            {
                ViewData["Navigation"] = INavigation.GetNavigation(UserInfo.T_Navigationls).JSONSerialize();

            }
         
          
            return View();
        }


    }
}
