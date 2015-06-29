using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtAop.BaseAttribute;
using MtAop.Context;

namespace Complex.Logical
{
  
    public class BaseEndAttribute : PostAspectAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            Console.WriteLine("log start!");
            return context;
        }
    }
}
