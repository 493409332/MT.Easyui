using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Complex.Logical.Admin.CacheManag;
using MtAop.Context;

namespace Complex.Logical.Admin.AopAttribute
{
    public class CacheAttribute : StartAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        { 
           context= base.Action(context);
           if ( new string[] { "RUser", "RRole", "RNavigation" }.Contains(context.ClassFullName.Split('.').Last()) && ( context.MethodName == "Add" || context.MethodName == "Edit" || context.MethodName == "Remove" || context.MethodName == "TrueRemove" || context.MethodName == "SetUseConfigrByKey" ) )
           {
               CacheManagement.Instance.RemoveStartsWith("T_UserInfo");
               var Session = HttpContext.Current.Session;
               Session["userinfo"] = null;
           }

            return context;
        }
    }
}
