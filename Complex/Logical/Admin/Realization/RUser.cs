using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
using Complex.Common.Utility.Extensions;
using Complex.Repository;
using Complex.Logical.Admin.AopAttribute;

namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RUser")]
    public class RUser : RBase<T_User>, IUser
    {
        public RUser()
            : base("MySQLServerContext")
        {
        }
        #region IUser 成员
        public List<T_UserTem> GetAllUser()
        {
            List<T_UserTem> quer = new List<T_UserTem>();
            //quer = EF.Database.SqlQuery<T_UserTem>("select a.*,b.DepartmentName from T_User a left join T_Department b on a.DepartmentId=b.ID").ToList();
            quer = EF.Database.SqlQuery<T_UserTem>("select a.* from T_User a where a.IsDelete='false'").ToList();
            return quer;
        }
        public T_User GetUserByKey(int UserID)
        {
            var quer = GetByKey(UserID);
            return quer;
        }
        [Cache]
        public int SetUseConfigrByKey(int UserID,string json)
        {
            var quer = GetByKey(UserID);
            quer.ConfigJson = json; 
            return SaveChanges();
        }
        public string GetConfig(int UserID)
        {
             var quer=GetAllNoCache().Where(p=>p.ID==UserID).FirstOrDefault();
             if ( quer!=null )
             {
                 return quer.ConfigJson != null ? quer.ConfigJson : "{ \"theme\": { \"title\": \"Bootstrap\", \"name\": \"bootstrap\" }, \"showType\": \"tree\", \"gridRows\": \"20\" }";
             } 
             return "{ \"theme\": { \"title\": \"Bootstrap\", \"name\": \"bootstrap\" }, \"showType\": \"tree\", \"gridRows\": \"20\" }";
        }
        
        #endregion
        //public List<T_Button> GetPage(string predicate, int page, int page_size, string order, string asc)
        //{
        //  //  return GetAllNoCache().OrderBy(p=>p.ID).Skip(( page - 1 ) * page_size).Take(page_size).ToList();

        //    return SearchSqLFor_Page<T_Button>("ID>1",2,5,"ID","asc"); 
        //}






    }
}
