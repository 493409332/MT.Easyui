using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Complex.Repository;

namespace Complex.Logical.Admin.Realization
{ 
    [ICOConfig("RLogin")]
    public class RLogin : RBase<T_User>, ILogin
    {
        public RLogin()
            : base("MySQLServerContext")
        {
        }
        #region ILogin 成员

        public bool SearchForName(string username)
        {
            return GetAllNoCache().Where(p => p.UserName == username).Count() > 0;

        }
        public T_User SearchForNameorPwd(string username, string userpwd)
        {
            return GetAllNoCache().Where(p => p.UserName == username && p.Password == userpwd).FirstOrDefault();
        }


        #endregion

      
    }
}
