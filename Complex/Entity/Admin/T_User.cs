using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Complex.Entity.Admin
{

    [Description("系统用户")]
    public class T_User : EntityBase
    { 

        [Description("用户名")]
        [Index("IX_UserName", IsUnique = true)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Description("密码")]
        [MaxLength(64)]
        public string Password { get; set; }
        [Description("真实姓名")]
        [MaxLength(30)]
  
        public string TrueName { get; set; }
        [Description("密码佐料")]
        [MaxLength(30)]
        public string PassSalt { get; set; }
        [MaxLength(80)]
        public string Email { get; set; }
        [Description("是否超管")]
        public bool IsAdmin { get; set; }
        [Description("是否禁用")]
        public bool IsDisabled { get; set; }
        [Description("部门Id")]
        public int DepartmentId { get; set; }
        [Description("手机")]
        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        public string QQ { get; set; }
        [Description("备注")]
        [MaxLength(500)]
        public string Remark { get; set; }
        [Description("个性化设置")]
        public string ConfigJson { get; set; }

        //聚合索引   
        //[Index("IX_UserName", IsUnique = true, Order = 1)] 
        //public string UserName { get; set; }  
        //[Index("IX_UserName", IsUnique = true, Order = 2)] 
        //public string TrueName { get; set; }
    }

    public class UserConfig {
        public theme theme { get; set; }
        public string showType { get; set; }
        public string gridRows { get; set; }
    }
    public class theme{
    
        public string title { get; set; }
        public string name { get; set; }
    }
  
}
