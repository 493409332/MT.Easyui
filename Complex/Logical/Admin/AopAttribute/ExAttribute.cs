using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtAop.Context;

namespace Complex.Logical.Admin.AopAttribute
{
    public class ExAttribute : BaseExAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            //Console.WriteLine("log exception!");

            //// context.Result
            ////  context.Result=Activator.CreateInstance(context.ResultType);
            //if ( context.ResultType == typeof(decimal) )
            //{
            //    context.Result = 111111.11111M;
            //}
            ////  throw context.Ex.Ex;


            return context;
        }
    }
}
