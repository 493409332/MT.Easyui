using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Mapping
{
    
    public class test2map : EntityTypeConfiguration<test2>
    {
        public test2map()
        {

            //// Table & Column Mappings
            this.ToTable("test2");
            this.Property(t => t.test3.Name1).HasColumnName("Name1");
            this.Property(t => t.test3.Num1).HasColumnName("Num1");
            //this.Property(t => t.UserID).HasColumnName("UserID"); 
        }

    }
}
