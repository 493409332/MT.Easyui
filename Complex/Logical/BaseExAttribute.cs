using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtAop.BaseAttribute;
using MtAop.Context;

namespace Complex.Logical
{
   public class BaseExAttribute : ExceptionAspectAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            Console.WriteLine("log exception!");

            throw context.Ex;

        }
    }
}
