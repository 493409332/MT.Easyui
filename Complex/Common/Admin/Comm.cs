using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Common.Admin
{
    public class Comm
    {
        public static string BuildToolbar()
        {
            var Request = System.Web.HttpContext.Current.Request;
            //var UserId = SysVisitor.Instance.UserId;
            //return UserBll.Instance.PageButtons(UserId, PublicMethod.GetInt(Request["navid"]));
            return "";
        }
        public static string isNULL(string s)
        {
            if ( string.IsNullOrWhiteSpace(s) )
                throw new Exception("值不能为空!");
            return s.Trim();
        }
       
    }
}
