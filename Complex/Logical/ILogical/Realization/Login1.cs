using Complex.Common.Utility;
using Complex.Entity;
using Complex.Logical.ILogical.AopAttribute;
using Complex.Logical.ILogical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
using Complex.EFRepository.Repository;
using Complex.Entity.Admin; 

namespace Complex.Logical.Realization
{
    //[ICOConfigAttrbute("Login1", typeof(Login1))]
    [ICOConfig("Login1")]
    public class Login1 : EFRepositoryBaseGeneric, ITest2
    {
        int ii = 1;
        Login test = new Login();
    
        //[Start]
        //public bool IsLogin(test2 model)
        //{
        //    ii = 2;
          
        //    return false;
        //}

        #region ILogin 成员

          [Start1]
        public decimal IsLogin(test2 model, int aa, decimal bb, object cc, float aaa)
      //  public bool IsLogin(test2 model, int aa)
            {
                Insert<test2>(model);
                Insert<T_Button>(new T_Button());
            ii = 2;

          
            test.OpenTransaction();
            try
            {
                test.IsLogin(model, aa, bb, cc, aaa);

            }
            catch ( Exception )
            {
                test.Rollback();
                throw;
            }
            test.Commit();


            return 1123.555M;
        }

        #endregion
    }
}
