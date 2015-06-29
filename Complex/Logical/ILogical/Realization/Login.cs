using Complex.Common.Utility;
using Complex.Entity;
using Complex.Logical.ILogical.AopAttribute;
using Complex.Logical.ILogical;
using Complex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
 

namespace Complex.Logical.Realization
{
    [ICOConfig("Login")]
    public class Login : EFRepositoryBase<test2>, ITest2
    {
        public Login() {
            ChangeDatabase("MySQLServerContext");
        }
        int ii = 1;
        //[Start]
        //[Ex]
        //public bool IsLogin(test2 model)
        //{ 
        //    ii = 2; 
        //    Insert(model);  
        //    return false;
        //}

        #region ILogin 成员

        
        [Start1]
        [AOPTransaction]
        [Ex]
        public decimal IsLogin(test2 model, int aa, decimal bb, object cc, float aaa)
     //   public bool IsLogin(test2 model, int aa)
        {


            
            Insert(model);

      
            var quer = EF.test2.AsNoTracking().OrderByDescending(p=>p.ID).ToList();
          // int iii= quer.RemoveAll(p=>p.ID>0);
         
            
        //   EF.SaveChanges();
            //Update(model);
            ii = aa;
           // Insert(model);
             throw new ArgumentOutOfRangeException("不存在的月份"); 
 
            return 123.222M;
        }

        #endregion
    }

    [ICOConfig("Loginc")]
    public class Loginc : EFRepositoryBase<test2>, ITest2<test2>
    {


        #region ITest2<test2> 成员
         [Start1]
        public decimal IsLogin(test2 model, int aa, decimal bb, object cc, float aaa)
        {
            return 100;
        }

        #endregion
    }
}
