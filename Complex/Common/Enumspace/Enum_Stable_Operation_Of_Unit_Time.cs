using Complex.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Common.Enumspace
{
    /// <summary>
    /// 机组稳定运行时间
    /// </summary>
    public enum Enum_Stable_Operation_Of_Unit_Time
    {
        /// <summary>
        /// 断路器合=1 
        /// </summary> 
         [Description("断路器合=1")]
        Circuit_breaker  = 9,
        /// <summary>
        /// 隔离刀闸合=1  
        /// </summary>
         [Description("隔离刀闸合=1")]
        Insulate_Disconnecting_link = 10,
        /// <summary>
        /// 定子电流>1  
        /// </summary>
        [Description("定子电流>1")]
        Stator_current = 11, 
        /// <summary>
        /// 当前水头出力稳定  
        /// </summary>
        [Description("当前水头出力稳定")]
        Water_head_output_stability = 12,
        /// <summary>
        ///交集类型时刻 
        /// </summary>
        [Description("交集类型时刻")]
        Intersection_types  = 13
    }
}
