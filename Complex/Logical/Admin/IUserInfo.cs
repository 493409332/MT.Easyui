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
    public interface IUserInfo : ITransientLifetimeManagerRegister 
    {
        T_UserInfo GetUserInfo(T_User User);
        T_UserInfo GetUserInfoByID(int ID);
        T_UserInfo GetUserInfoBySession();
    }
}
