using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Utility;


namespace Complex.Entity.Admin
{
    //[TableName("sys_Navigations")]
    [Description("导航菜单")]
    public class T_Navigation : EntityBase
    { 
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Description("菜单名称")]
        [MaxLength(50)]
        public string NavTitle { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [Description("链接地址")]
        [MaxLength(200)]
        public string Linkurl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        public int Sortnum { get; set; }
        /// <summary>
        /// "图标CSS
        /// </summary>
        [Description("图标CSS")]
        [MaxLength(50)]
        public string iconCls { get; set; }
        /// <summary>
        /// 图标URL
        /// </summary>
        [Description("图标URL")]
        [MaxLength(100)]
        public string iconUrl { get; set; }


        private bool _IsVisible = true;
        /// <summary>
        /// 是否显示
        /// </summary> 
        [Description("是否显示")]
        //[ System.ComponentModel.DefaultValue(true)] 
        public bool IsVisible {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        /// <summary>
        /// 父ID
        /// </summary>
        [Description("父ID")]
        public int ParentID { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Description("菜单标识")]
        [MaxLength(50)]
        public string NavTag { get; set; }
        /// <summary>
        /// 大图标路径
        /// </summary>
        [Description("大图标路径")]
        [MaxLength(100)]
        public string BigImageUrl { get; set; }
        /// <summary>
        /// 是否为系统菜单
        /// </summary> 
        
        [Description("是否为系统菜单")]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsSys { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        [NotMapped]
        public IEnumerable<T_Navigation> children
        {
            get;
            set;
        }    
        /// <summary>
        /// 所有按钮
        /// </summary>
        [NotMapped]
        public List<string> AllButtonHtmlList
        {
            get;
            set;
        }

        //[DbField(false)]
        //public IEnumerable<Button> Buttons
        //{
        //    get { return ButtonDal.Instance.GetButtonsBy(KeyId); }
        //}

        /// <summary>
        ///已选按钮
        /// </summary>
        [NotMapped]
        public List<string> OwnedBut { get; set; }

        /// <summary>
        ///按钮HTML
        /// </summary>
        [NotMapped]
        public List<string> ButtonHtmlList
        { get; set; }
        /// <summary>
        /// 供角色授权使用
        /// </summary>
         [NotMapped]
        public List<string> Buttons { get; set; }
    }
}
