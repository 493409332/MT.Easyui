using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Logical.Admin.AopAttribute
{
    public class UniqueExAttribute : ExAttribute
    {                    
        public override MtAop.Context.InvokeContext Action(MtAop.Context.InvokeContext context)
        {
           
           // context.Ex.InnerException.InnerException
            base.Action(context);
            if ( context.ClassFullName.StartsWith("Complex.Logical.Admin.Realization") && ( context.MethodName == "Add" || context.MethodName == "Edit" ) )
            { 
                var innerex = context.Ex.InnerException.InnerException;
                if ( innerex != null )
                {
                    if ( innerex.Message.Contains("IX_UserName") || innerex.Message.Contains("IX_ButtonTag") )
                    {
                        context.Result = Complex.Common.Enumspace.CRUDState.UniqueErro;
                    }
                }
            }
            return context;
        }
    }
}
