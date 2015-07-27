using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
using Complex.Logical.Admin.AopAttribute;
using Complex.Repository;

namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RUserRoles")]
    public class RUserRoles : RBase<T_UserRoles>, IUserRoles
    {
        public RUserRoles()
            : base("MySQLServerContext")
        {

        }

        //public List<T_Button> GetPage(string predicate, int page, int page_size, string order, string asc)
        //{
        //  //  return GetAllNoCache().OrderBy(p=>p.ID).Skip(( page - 1 ) * page_size).Take(page_size).ToList();

        //    return SearchSqLFor_Page<T_Button>("ID>1",2,5,"ID","asc"); 
        //}

        public List<T_UserRoles> GetRoleByUserID(int UserID)
        {
            return GetAllNoCache().Where(p => p.UserID == UserID).ToList();
        }

        [AOPTransaction]
        [Log]
        public int AddUserTo(int UserID, int[] roleids)
        {
            var result = true;
            var Error = 0;
            if (GetAllNoCache().Where(p => p.UserID == UserID).Count() > 0)
            {
                var rows = EF.Database.ExecuteSqlCommand("delete from T_UserRoles where UserID=" + UserID);
                if (rows == 0)
                {
                    Error++;
                }
            }
            List<T_UserRoles> models = new List<T_UserRoles>();
            foreach (var item in roleids)
            {
                T_UserRoles model = new T_UserRoles();
                model.UserID = UserID;
                model.RoleID = item;
                models.Add(model);
            }
            if (Insert(models) < models.Count())
            {
                Error++;
            }
            if (Error != 0)
            {
                Rollback();
                result = false;
            }

            return result ? 1 : 0;
        }
    }
}
