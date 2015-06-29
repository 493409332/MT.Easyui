using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Complex.ICO_AOP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Logical.Admin
{
    [ICO_AOPEnable(true)]
    public interface IRole : ITransientLifetimeManagerRegister, IBase<T_Role>
    {
        List<T_Role> GetPage(T_Role model, int page, int page_size, string order, string asc, out int total);
        List<T_Role> GetAllRole();
    }
}
