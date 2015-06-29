using Complex.Common.Utility;
using Complex.Entity;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.ICO_AOP;
using Complex.ICO_AOP.Utility;
using Complex.ICO_AOP.Attribute;
 

namespace Complex.Logical.ILogical
{
    [ICO_AOPEnable(true)]
    public interface ITest2 : ITransientLifetimeManagerRegister
    {
        decimal IsLogin(test2 model, int aa, decimal bb, object cc, float aaa);
      //  bool IsLogin(test2 model, int aa); 
    }

    [ICO_AOPEnable(true)]
    public interface ITest2<T> : ITransientLifetimeManagerRegister
    {
        decimal IsLogin(T model, int aa, decimal bb, object cc, float aaa);
        //  bool IsLogin(test2 model, int aa); 
    }
}
