using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtAop.Context;

namespace Complex
{
    class Class1
    {
        public decimal aa()
        {
            decimal aaa=0;



            IEnumerable<Class1> aaaa = new List<Class1>();
            List<Class1> bbbb = (List<Class1>) aaaa;
            return aaa;
        }
    }
}
