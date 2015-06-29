using MtAop.BaseAttribute;
using MtAop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Logical.ILogical.AopAttribute
{
   public class EndAttribute : PostAspectAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            Console.WriteLine("log start!");
            return context;
        }
    }
}
