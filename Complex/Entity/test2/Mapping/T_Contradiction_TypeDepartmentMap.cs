using Complex.Entity.Join;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Mapping
{
    public class T_Contradiction_TypeDepartmentMap : EntityTypeConfiguration<T_Contradiction_TypeDepartment>
    {

        public T_Contradiction_TypeDepartmentMap()
        {
            this.ToTable("View_T_Contradiction_TypeDepartment");
        }


    }
}
