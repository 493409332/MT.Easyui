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
    public interface IDepartment : ITransientLifetimeManagerRegister, IBase<T_Department>
    {
        List<T_Department> GetTreeGrid(int ParentID);
        List<T_Department> GetDepartment(int ParentID);
        String GetDepartmentName(int DepartmentID);
    }
}
