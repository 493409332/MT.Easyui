using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Repository;
 

namespace Complex.BLL
{
    /// <summary>
    /// 业务基类对象
    /// </summary>
    /// <typeparam name="T">业务对象类型</typeparam>
   public interface  BaseLogic<T> where T : class, new()
    { 
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行操作是否成功。</returns>
         bool Insert(T info,int userid);
       /// <summary>
       /// 分页
       /// </summary>
       /// <param name="info"></param>
       /// <param name="page"></param>
       /// <param name="rows"></param>
       /// <param name="sort"></param>
       /// <param name="order"></param>
       /// <returns></returns>
         object GetPage(T info, int page, int rows, string sort, string order);
       
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
         bool Update(T info, int userid);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
         bool Delete(T info, int userid);
    }
}
