using Complex.Entity.Join;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Mapping
{
    public class T_Contradiction_CaseOrGridInfoMap : EntityTypeConfiguration<T_Contradiction_CaseOrGridInfo> 
    {
        public T_Contradiction_CaseOrGridInfoMap()
        {
 
            //// Table & Column Mappings
            this.ToTable("View_T_Contradiction_CaseOrGridInfo");
            //this.Property(t => t.UserID).HasColumnName("UserID");
          

        }

    }
}
