using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Data;
using Complex.ICO_AOP.Attribute;
using Complex.Logical.Admin.AopAttribute;
using Complex.Repository;

namespace Complex.Logical.Admin.Realization
{
    public abstract class RBase<T> : EFRepositoryBase<T>, IBase<T> where T : Complex.Entity.EntityBase
    {
        public RBase()
            : base("MySQLServerContext")
        {


        }
        public RBase(string strsql)
            : base(strsql)
        {

        }
        #region IBase<T> 成员
        [OpenBase]
        [UniqueEx]
        [Log]
        public virtual int Add(T model)
        {
            return Insert(model);
        }
        [OpenBase]
        [Log]
        public virtual int Remove(int ID)
        {
            var quer = GetAll().Where(p => p.ID == ID).FirstOrDefault();
            if ( quer == null )
            {
                throw new Exception("找不到该数据");
            }
            quer.IsDelete = true;
            return SaveChanges(); 
        }
        [OpenBase]
        [Log]
        public int TrueRemove(int ID)
        {
            return DeleteByKey(ID);
        }

        [OpenBase]
        [UniqueEx]
        [Log]
        public virtual int Edit(T model)
        {
            return Update(model);
        }
        [OpenBase]
        public virtual List<T> GetAllList()
        {
            return GetAllNoCache().Where(p=>p.IsDelete==false).ToList();
        }
        [OpenBase]
        public virtual IEnumerable<T> GetAllEnumerable()
        {
            return GetAllNoCache();
        }
         [OpenBase]
         [Start]
        public virtual List<T> GetPage(string predicate, int page, int page_size, string order, string asc,out int total)
        {
            //total =GetAllNoCache().Where(p=>p.IsDelete==false).Count();
            //predicate= ;
            return SearchSqLFor_Page(FilterTranslator.ToSql(predicate), page, page_size, order, asc, out total);
        }

        [OpenBase]
        public virtual List<T> GetPageList(T model, int page, int rows, string sort, string order, out int total, string[] where)
        {
            throw new NotImplementedException();
        }
        [OpenBase]
        public virtual IEnumerable<T> GetPageEnumerable(T model, int page, int rows, string sort, string order, out int total, string[] where)
        {
            throw new NotImplementedException();
        }
        #endregion










        
    }
}
