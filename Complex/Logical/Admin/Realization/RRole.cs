using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Complex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complex.Common.Utility.Extensions;
using System.Threading.Tasks;

namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RRole")]
    public class RROLE : RBase<T_Role>, IRole
    {
        public RROLE()
            : base("MySQLServerContext")
        {

        }

        public List<T_Role> GetPageList(T_Role model, int page, int rows, string sort, string order, out int total, object where)
        {

            return GetPageList(model, page, rows, sort, order, out total, where);
        }
       
        public IEnumerable<T_Role> GetPageEnumerable(T_Role model, int page, int rows, string sort, string order, out int total, object where)
        {
            return GetPageEnumerable(model, page, rows, sort, order, out total, where);
        }

        public List<T_Role> GetAllList(T_Role model, int page, int rows, string sort, string order, out int total)
        {
            return GetAllList(model, page, rows, sort, order, out total);
        }

        public IEnumerable<T_Role> GetAllEnumerable(T_Role model, int page, int rows, string sort, string order, out int total)
        {
            return GetAllEnumerable(model, page, rows, sort, order, out total);
        }

        //public List<T_Role> GetPage(T_Role model, int page, int rows, string sort, string order, out int total, string param)
        //{
        //    var quer = GetAllNoCache();
        //    if ( !param.IsNullOrEmpty() )
        //    {
        //        quer = quer.Where(p => p.ID == 1);
        //    }
        //    total = quer.Select(p => p.ID).Count();
        //    return quer.Skip(( page - 1 ) * rows).Take(rows).ToList();
        //}
        
        public List<T_Role> GetPage(T_Role model, int page, int page_size, string order, string asc, out int total)
        {
            throw new NotImplementedException();
        }

        public List<T_Role> GetAllRole() {

            return GetAllNoCache().ToList();
        }
        [Complex.Logical.Admin.AopAttribute.Start]
        public override List<T_Role> GetPage(string predicate, int page, int page_size, string order, string asc, out int total)
        {
            return base.GetPage(predicate, page, page_size, order, asc, out total);
        }
    }
}