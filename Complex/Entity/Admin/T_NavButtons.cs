using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Admin
{
    public class T_NavButtons : EntityBase
    {
        public int NavId { get; set; }
        public int ButtonId { get; set; }
        public int Sortnum { get; set; }
    }
}
