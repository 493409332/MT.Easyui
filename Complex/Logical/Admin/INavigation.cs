using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Complex.ICO_AOP.Utility;

namespace Complex.Logical.Admin
{
    [ICO_AOPEnable(true)]
    public interface INavigation : ITransientLifetimeManagerRegister, IBase<T_Navigation>
    {
        List<T_Navigation> GetTreeGrid(int ParentID);
        object GetNavigation();
        object GetNavigation(List<T_Navigation> list);
    }
}
