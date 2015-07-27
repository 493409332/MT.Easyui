using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Enumspace;
using Complex.ICO_AOP.Attribute;
using Complex.Mongodb.Entity;
using Complex.Mongodb.Utility;
using MongoDB;
using Complex.Common.LinqEx;
namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RAdminLog")]
    public class RAdminLog : IAdminLog
    {



        #region IAdminLog 成员

        public List<Log> GetPage(string predicate, int page, int page_size, string order, string asc, string runclass, string username, out int total)
        {
            using ( MongoDBUtility db = new MongoDBUtility() )
            {
                var quer = db.GetIMongoCollection<Log>().Linq();

  
                if (runclass!=null&&runclass.Length>0)
	            {
                    quer = quer.Where(p => p.RunClassName == runclass);
                }

                if ( username != null && username.Length > 0 )
                {
                    quer = quer.Where(p => p.UserName == username);
                }
 
                quer = quer.OrderSort(order, asc).ThenOrderSort(order, asc);

 
                total = quer.Count();

                return quer.Skip(( page - 1 ) * page_size).Take(page_size).ToList();
            }


        }


        //public Expression<Func<T, D>> GenerateOrder<T, D>(string order)
        //{
        //    ParameterExpression orderex = Expression.Parameter(typeof(T), "orderex");
        //    Expression<Func<T, D>> field =
        //         Expression.Lambda<Func<T, D>>(
        //             Expression.Property(
        //                 orderex,
        //                order
        //             ),
        //             new ParameterExpression[] { orderex }
        //         );
        //    return field;
        //}



        public object GetClassList()
        {
            using ( MongoDBUtility db = new MongoDBUtility() )
            {
                var quer = db.GetIMongoCollection<Log>().Linq().Select(p => p.RunClassName).ToList();
                return quer.GroupBy(p => p).Select(p =>
                    { return new { name = p.Key.Split('.').Last(), value = p.Key }; }).ToList();
            }
        }


        public bool Delete()
        {

            using ( MongoDBUtility db = new MongoDBUtility() )
            {
                db.GetIMongoCollection<Log>().Remove(p => p.GuID != null);
                return db.GetIMongoCollection<Log>().Count() == 0;
            }

        }
        public object GetUserNamelist()
        {
            using ( MongoDBUtility db = new MongoDBUtility() )
            {
                var quer = db.GetIMongoCollection<Log>().Linq().Select(p => p.UserName).ToList();
                return quer.GroupBy(p => p).Select(p =>
                { return new { name = p.Key, value = p.Key }; }).ToList();
            }
        }
        #endregion
    }
 
}
