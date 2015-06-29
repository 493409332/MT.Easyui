using Complex.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Complex.Common.Enumspace
{
    public enum OperationType
    {
        /// <summary>
        /// 添加
        /// </summary> 
        [Description("添加")]
        Insert = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 2,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 4,
        /// <summary>
        ///登录
        /// </summary>
        [Description("登录")]
        Select = 8
    }
}
