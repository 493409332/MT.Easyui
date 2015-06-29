using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Utility;

namespace Complex.Common.Enumspace
{
   public enum  CRUDState
    {
       /// <summary>
        /// 违反唯一约束添加已存在
       /// </summary>
       [Description("违反唯一约束！")]
       UniqueErro = -1
    }
}
