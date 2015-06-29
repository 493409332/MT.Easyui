using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Admin
{
    public class T_UserInfo
    {
        public T_User T_User { get; set; }

        public List<T_Role> T_Rolels { get; set; }

        public List<T_Navigation> T_Navigationls { get; set; }

        public List<T_Navigation> AllT_Navigationls { get; set; } 
    }
}
