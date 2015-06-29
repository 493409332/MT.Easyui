using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
using Complex.Repository;

namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RButton")]
    public class RButton : RBase<T_Button>, IButton
    {
        public RButton()
            : base("MySQLServerContext")
        { 
        }

        //public List<T_Button> GetPage(string predicate, int page, int page_size, string order, string asc)
        //{
        //  //  return GetAllNoCache().OrderBy(p=>p.ID).Skip(( page - 1 ) * page_size).Take(page_size).ToList();
           
        //    return SearchSqLFor_Page<T_Button>("ID>1",2,5,"ID","asc"); 
        //}


        public List<T_Button> GetButtons()
        {
            return GetAllNoCache().OrderBy(p => p.ID).ToList();
        }
        
    }
}
