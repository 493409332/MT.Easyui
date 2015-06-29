using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Common.Utility;


namespace Complex.Entity.Admin
{
    
   
    [Description("角色管理")]
    public class T_Role : EntityBase
    {
          

        [Description("角色名称")]
        [MaxLength(50)]
        public string RoleName { get; set; }
 
        [Description("排序")]
        public int Sortnum { get; set; }
        [Description("描述")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [Description("是否为默认角色")]
        public int IsDefault { get; set; }

        [Description("部门Id")]
        public int DepartmentId { get; set; }
 
        //public IEnumerable<Navigation> Navigations { get; set; }

     
        //public IEnumerable<User> Users { get; set; }


        ///// <summary>
        ///// 角色可以访问的部门列表
        ///// </summary>
      
        //public string Departments
        //{
        //    get { return RoleDal.Instance.GetDepIDs(KeyId); }
        //}
    }
}
