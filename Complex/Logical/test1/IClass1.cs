using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.ICO_AOP.Attribute;
using Complex.ICO_AOP.Utility;

namespace Complex.Logical.test1
{
     
    [ICO_AOPEnable(true)] 
    public interface IClass1 : ITransientLifetimeManagerRegister
    {
       int Add(Complex.Entity.test1.Class1 model);
    }
}
