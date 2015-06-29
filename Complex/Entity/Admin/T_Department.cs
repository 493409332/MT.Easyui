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

    [Table("T_Department")]
    [Description("部门管理")]
    public class T_Department : EntityBase
    {
       
    
        [Description("部门名称")]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

       
        [Description("上级ID")]
        public int ParentId { get; set; }

       
        [Description("排序")]
        public int Sortnum { get; set; }

        [Description("备注")]
        [MaxLength(500)]
        public string Remark { get; set; }
       
        [NotMapped]
        public IEnumerable<T_Department> children
        {
            get;
            set;
        }

        [NotMapped]
        public string text
        {
            get;
            set;
        }
        // [NotMapped]
        ///// <summary>
        ///// tree 节点状态
        ///// </summary>
        //public string state
        //{
        //    get
        //    {
        //        if ( ParentId == 0 )
        //            return "open";
        //        return children.Any() ? "closed" : "open";
        //    }
        //}
    }
}
