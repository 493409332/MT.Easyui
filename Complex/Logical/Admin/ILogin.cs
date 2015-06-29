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
    public interface ILogin : IBase<T_User>, ITransientLifetimeManagerRegister
    {
        bool SearchForName(string username);
        T_User SearchForNameorPwd(string username, string userpwd);
    }
}
