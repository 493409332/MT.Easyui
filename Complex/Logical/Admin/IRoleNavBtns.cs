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
    public interface IRoleNavBtns : ITransientLifetimeManagerRegister, IBase<T_RoleNavBtns>
    {
       List<T_RoleNavBtns> GetRoleNavBtns();
       List<T_RoleNavBtns> GetRoleNavBtnsByRole(int roleID);
       bool setRoleButtons(string Data);
    }
}
