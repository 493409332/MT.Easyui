using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;

namespace Complex.Logical.Admin.Realization
{

    [ICOConfig("RNavButtons")]
    public class RNavButtons : RBase<T_NavButtons>, INavButtons
    {
        public RNavButtons()
            : base("MySQLServerContext")
        {
        }

        #region INavButtons 成员

        public List<T_NavButtons> GetButByNavID(int NavID)
        {
            return GetAllNoCache().Where(p => p.NavId == NavID && p.IsDelete == false).ToList();
        }
        [AOPTransaction]
        public bool setButtons(int NavID, int[] btns)
        {
            int Erro = 0;
            bool ret = true;
            if ( btns.Length > 0 && NavID > 0 )
            { 
                if ( GetAllNoCache().Where(p => p.NavId == NavID).Count() > 0 )
                {
                  if ( EF.Database.ExecuteSqlCommand("delete from T_NavButtons where NavId=" + NavID)==0)
                    {
                        Erro++;
                    }
                } 
                foreach ( var item in btns )
                {
                    if ( Add(new T_NavButtons() { NavId = NavID, ButtonId = item }) < 1 )
                    {
                        Erro++;
                    }
                }
                if ( Erro != 0 )
                {
                    Rollback();
                    ret = false;
                }
            }
            return ret;
        }
        #endregion
    }
}
