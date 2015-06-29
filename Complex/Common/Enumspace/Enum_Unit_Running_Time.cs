using Complex.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Common.Enumspace
{
    /// <summary>
    /// 机组运行时间
    /// </summary>
    public enum Enum_Unit_Running_Time
    {
        /// <summary>
        /// 转速类型时刻
        /// </summary>
        [Description("转速类型时刻")]
        Speed = 1,
        /// <summary>
        /// 涡壳压力类型时刻  
        /// </summary>
        [Description("涡壳压力类型时刻")]
        Volute_pressure = 2,
        /// <summary>
        /// 导叶全关类型时刻 
        /// </summary>
        [Description("导叶全关类型时刻")]
        Full_closed_wicket = 3,
        /// <summary>
        ///交集类型时刻 
        /// </summary>
        [Description("交集类型时刻")]
        Intersection_types = 4

    }
}
