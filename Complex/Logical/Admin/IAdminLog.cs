using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.ICO_AOP.Attribute;
using Complex.ICO_AOP.Utility;
using Complex.Mongodb.Entity;

namespace Complex.Logical.Admin
{
    [ICO_AOPEnable(true)]
    public interface IAdminLog : ITransientLifetimeManagerRegister
    {
        List<Log> GetPage(string predicate, int page, int page_size, string order, string asc, string runclass, string username, out int total);
         bool Delete();
         object GetClassList();
         object GetUserNamelist();
         
    }
}
