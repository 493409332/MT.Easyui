using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.Logical.Admin;
using Complex.Logical.Admin.Realization;
using MtAop.BaseAttribute;
using MtAop.Context;
 

namespace Test.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Type[] typ = new Type[2] ;
            typ[0] = typeof(string);
            typ[1] = typeof(int);

            DynamicRROLEType aaa = new DynamicRROLEType();
            int ii=0;
            Console.WriteLine(aaa.GetPage(new Complex.Entity.Admin.T_Role(), 1, 1, "1", "1", ref ii).Count());
            Console.WriteLine(aaa.GetPage("", 1, 1, "", "", ref ii).Count());
        }
    }

    public sealed class DynamicRROLEType  
    {
        // Fields
        private RROLE _realProxy = null;

        // Methods
        public DynamicRROLEType()
        {
            this._realProxy = new RROLE();
        }

  
        public   List<T_Role> GetPage(T_Role role1, int num1, int num2, string text1, string text2, ref int numRef1)
        {
            InvokeContext context = new InvokeContext();
            context.SetMethod("GetPage");
            context.SetClassName("Complex.Logical.Admin.Realization.RROLE");
            context.ResultType = typeof(List<T_Role>);
            List<T_Role> result = null;
            Exception e = null;
            context.SetParameter(role1);
            context.SetParameter(num1);
            context.SetParameter(num2);
            context.SetParameter(text1);
            context.SetParameter(text2);
            context.SetParameter(numRef1);

            Type[] types = new Type[] { typeof(T_Role), typeof(int), typeof(int), typeof(string), typeof(string), typeof(int).MakeByRefType() };
            MethodInfo method = this._realProxy.GetType().GetMethod("GetPage", types);
            PreAspectAttribute customAttribute = (PreAspectAttribute) Attribute.GetCustomAttribute(method, typeof(PreAspectAttribute));
            if ( customAttribute != null )
            {
                context = customAttribute.Action(context);
            }
            if ( !context.IsRun )
            {
                return null;
            }
            try
            {
                result = this._realProxy.GetPage((T_Role) context.Parameters[0], (int) context.Parameters[1], (int) context.Parameters[2], (string) context.Parameters[3], (string) context.Parameters[4], out numRef1);
                context.SetResult(result);
                PostAspectAttribute attribute2 = (PostAspectAttribute) Attribute.GetCustomAttribute(method, typeof(PostAspectAttribute));
                if ( attribute2 != null )
                {
                    context = attribute2.Action(context);
                    result = (List<T_Role>) context.Result;
                }
            }
            catch ( Exception exception1 )
            {
                e = exception1;
                context.SetError(e);
                ExceptionAspectAttribute attribute3 = (ExceptionAspectAttribute) Attribute.GetCustomAttribute(method, typeof(ExceptionAspectAttribute));
                if ( attribute3 == null )
                {
                    throw e;
                }
                return (List<T_Role>) attribute3.Action(context).Result;
            }
            return result;
        }

        public   List<T_Role> GetPage(string text1, int num1, int num2, string text2, string text3, ref int numRef1)
        {
            InvokeContext context = new InvokeContext();
            context.SetMethod("GetPage");
            context.SetClassName("Complex.Logical.Admin.Realization.RROLE");
            context.ResultType = typeof(List<T_Role>);
            List<T_Role> result = null;
            Exception e = null;
            context.SetParameter(text1);
            context.SetParameter(num1);
            context.SetParameter(num2);
            context.SetParameter(text2);
            context.SetParameter(text3);
            context.SetParameter(numRef1);

            MethodInfo method = this._realProxy.GetType().GetMethod("GetPage");
            PreAspectAttribute customAttribute = (PreAspectAttribute) Attribute.GetCustomAttribute(method, typeof(PreAspectAttribute));
            if ( customAttribute != null )
            {
                context = customAttribute.Action(context);
            }
            if ( !context.IsRun )
            {
                return null;
            }
            try
            {
                result = this._realProxy.GetPage((string) context.Parameters[0], (int) context.Parameters[1], (int) context.Parameters[2], (string) context.Parameters[3], (string) context.Parameters[4], out numRef1);
                context.SetResult(result);
                PostAspectAttribute attribute2 = (PostAspectAttribute) Attribute.GetCustomAttribute(method, typeof(PostAspectAttribute));
                if ( attribute2 != null )
                {
                    context = attribute2.Action(context);
                    result = (List<T_Role>) context.Result;
                }
            }
            catch ( Exception exception1 )
            {
                e = exception1;
                context.SetError(e);
                ExceptionAspectAttribute attribute3 = (ExceptionAspectAttribute) Attribute.GetCustomAttribute(method, typeof(ExceptionAspectAttribute));
                if ( attribute3 == null )
                {
                    throw e;
                }
                return (List<T_Role>) attribute3.Action(context).Result;
            }
            return result;
        }

    
    }

 

}
