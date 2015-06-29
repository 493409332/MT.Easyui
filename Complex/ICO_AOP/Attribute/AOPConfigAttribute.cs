using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.ICO_AOP.Attribute
{
    /// <summary>
    /// 开始事务
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AOPTransactionAttribute : System.Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="baseType"></param>
        public AOPTransactionAttribute(bool transactionEnable = true)
        {
            TransactionEnable = transactionEnable;
        }

        /// <summary>
        /// 父类类型 开启需要继承 EFRepositoryBase
        /// </summary>
        public bool TransactionEnable { get; set; }
 
    }
}
