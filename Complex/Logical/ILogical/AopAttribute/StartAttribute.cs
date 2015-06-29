using MtAop.BaseAttribute;
using MtAop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity;
namespace Complex.Logical.ILogical.AopAttribute
{
    public class StartAttribute : PreAspectAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            for ( int ii = 0; ii < context.Parameters.Count;ii++ )
            {
                //if ( item is test2 )
                //{
                //    test2 aaa = (test2) item;
                //    if ( aaa.ID == 1 )
                //    {
                //        aaa.ID = 100;
                //    }

                //}
                if ( context.Parameters[ii] is test2 )
                {
                    test2 aaa=new test2();

                    aaa = context.Parameters[ii] as test2;
                    if ( aaa.ID == 1 )
                    {
                        aaa.ID = 100;
                    }
                }
                if ( context.Parameters[ii] is int )
                { 
                    context.Parameters[ii] = 1111;
                }

            }

            Console.WriteLine("log start!");
           context.IsRun = true;
            return context;
        }
    }
    public class Start1Attribute : PreAspectAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            if ( context.ClassFullName == "Complex.Logical.Realization.Login" && context.MethodName == "IsLogin" )
            {


                for ( int ii = 0; ii < context.Parameters.Count; ii++ )
                {

                    if ( context.Parameters[ii] is test2 )
                    {

                        context.Parameters[ii] = new test2() { ID = 100, Name = "呵呵", Num = 100, test3 = new test3() { Name1 = "呵呵1", Num1 = 100 } };

                    }
                    if ( context.Parameters[ii] is int )
                    {
                        context.Parameters[ii] = 1111;
                    }

                }
            }
            Console.WriteLine("log start!");
            context.IsRun = true;
            return context;
        }
    }
}
