using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.test1;
using Complex.ICO_AOP.Attribute;
using Complex.Logical.test1.AopAttribute;
using Complex.Repository;

namespace Complex.Logical.test1.Realization
{
    [ICOConfig("classs1")]
    public class RClass1 : EFRepositoryBase<Class1>, IClass1
    {

        #region IClass1 成员
         //[Start]
         //[AOPTransaction]
         //[Ex]
         //[End]
        public int Add(Class1 model)
        {
            return Insert(model);
        }

        #endregion
    }
}
