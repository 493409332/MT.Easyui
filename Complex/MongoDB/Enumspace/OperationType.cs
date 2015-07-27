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
        /// 逻辑删除
        /// </summary>
        [Description("逻辑删除")]
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
        Select = 8,

        /// <summary>
        ///物理删除
        /// </summary>
        [Description("物理删除")]
        TrueDelete = 16,

        /// <summary>
        ///分配角色
        /// </summary>
        [Description("分配角色")]
        UserRolesUpdate = 17,

        /// <summary>
        ///设置角色菜单按钮关系
        /// </summary>
        [Description("设置角色菜单按钮关系")]
        RoleNavButtons = 18,

        /// <summary>
        ///设置菜单按钮关系
        /// </summary>
        [Description("设置菜单按钮关系")]
        NavButtons = 19,

        /// <summary>
        ///用户配置修改
        /// </summary>
        [Description("用户配置修改")]
        UseConfigrByKey = 20

        

    }
}
