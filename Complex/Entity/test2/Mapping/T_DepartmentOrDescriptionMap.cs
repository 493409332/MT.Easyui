using Complex.Entity.Join;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Mapping
{ 
     public class T_DepartmentOrDescriptionMap : EntityTypeConfiguration<T_DepartmentOrDescription> 
    {
         public T_DepartmentOrDescriptionMap()
        {
 
            //// Table & Column Mappings
            this.ToTable("View_T_DepartmentOrDescription");
            //this.Property(t => t.UserID).HasColumnName("UserID"); 
        }

    }
}
