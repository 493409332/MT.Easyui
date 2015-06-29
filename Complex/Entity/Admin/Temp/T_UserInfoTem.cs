using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Entity.Admin.Temp
{
  
    public class T_UserInfoTem
    {
        public T_UserInfoTem(List<T_UserInfoTem> list)
        {
            listdata = list;
        }
        public List<T_UserInfoTem> listdata { get; set; }
        public int id { get; set; }

        public string text { get; set; }

        public string iconCls { get; set; }
        
        public attributes attributes { get; set; }

        public List<T_UserInfoTem> children
        {
            get
            {
                return listdata.Where(p => p.id == p.attributes.parentid).ToList();
            }
        }
   }
   public class attributes
   {
       public string url { get; set; }

       public string iconUrl { get; set; }

       public int parentid { get; set; }

       public int sortnum { get; set; }

       public string BigImageUrl { get; set; }
   }
}
