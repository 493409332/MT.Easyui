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
    public interface INavButtons : ITransientLifetimeManagerRegister, IBase<T_NavButtons>
    {
        List<T_NavButtons> GetButByNavID(int NavID);
        bool setButtons(int NavID, int[] btns);
    }
}
