using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
using Complex.ICO_AOP.Utility;

namespace Complex.Logical.Admin
{
    [ICO_AOPEnable(true)]
    public interface IButton : ITransientLifetimeManagerRegister, IBase<T_Button>
    {
        List<T_Button> GetButtons();
    }
}
