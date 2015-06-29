using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity
{
   
   public class test2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        [ConcurrencyCheck]
        public test3 test3 { get; set; }
       
    }

   public class test3
   { 
       public string Name1 { get; set; }
       public int Num1 { get; set; }
   }
}
