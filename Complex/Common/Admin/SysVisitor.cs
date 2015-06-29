using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Complex.Common.Admin
{
    public class SysVisitor
    {

        public static SysVisitor Instance
        {
            get { return SingletonProvider<SysVisitor>.Instance; }
        }

        ///// <summary>
        ///// 用户ID
        ///// </summary>
        //public int UserId
        //{
        //    get { return PublicMethod.GetInt(HttpContext.Current.Session[SessionUserIdKey]); }
        //    set { HttpContext.Current.Session[SessionUserIdKey] = value; }
        //}
 

    }
}
