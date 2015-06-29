using MtAop.BaseAttribute;
using MtAop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Complex.Logical.ILogical.AopAttribute
{
    class ExAttribute : ExceptionAspectAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            Console.WriteLine("log exception!");

           // context.Result
          //  context.Result=Activator.CreateInstance(context.ResultType);
            if ( context.ResultType == typeof(decimal) )
            {
                context.Result = 111111.11111M;
            }
         //  throw context.Ex.Ex;
            return context;
        }
    }
}
