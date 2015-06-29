using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.ICO_AOP.Attribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ICOConfigAttribute : System.Attribute
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="description"></param>
       /// <param name="baseType"></param>
        public ICOConfigAttribute(string description="" )
        { 
            this.Description = description;
          //  TransactionEnable = transactionEnable;
        }

        ///// <summary>
        ///// 父类类型 开启需要继承 EFRepositoryBase
        ///// </summary>
        //public bool TransactionEnable { get; set; }



        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
