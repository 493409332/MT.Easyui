using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Utility;


namespace Complex.Entity.Admin
{
    /// <summary>
    /// 用户个性化设置
    /// </summary>
    public class ConfigModel : EntityBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 皮肤
        /// </summary>
        [Description("皮肤")]
        public int ThemeID { get; set; }

        /// <summary>
        /// 表现方式
        /// </summary>
        public string ShowType { get; set; }

        /// <summary>
        /// Grid 每页显示记录数
        /// </summary>
        public int GridRows { get; set; }

    }

    public class Theme : EntityBase
    { 
        /// <summary>
        /// 主题名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 主题路径名称
        /// </summary>
        public string Name { get; set; }
    }
}
