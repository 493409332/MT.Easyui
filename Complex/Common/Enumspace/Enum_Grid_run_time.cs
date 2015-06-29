using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Utility;
namespace Complex.Common.Enumspace
{
    /// <summary>
    /// 并网运行时间
    /// </summary>
    public enum Enum_Grid_run_time
    {
        /// <summary>
        /// 断路器合=1 
        /// </summary> 
        [Description("断路器合=1")]
        Circuit_breaker  = 5,
        /// <summary>
        /// 隔离刀闸合=1  
        /// </summary>
         [Description("隔离刀闸合=1")]
        Insulate_Disconnecting_link = 6,
        /// <summary>
        /// 定子电流>100    
        /// </summary>
         [Description("定子电流>100")]
        Stator_current = 7,  
        /// <summary>
        ///交集类型时刻 
        /// </summary>
        [Description("交集类型时刻")]
        Intersection_types  = 8

    }
}
