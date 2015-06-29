using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Complex.Entity
{
	/// <summary>
	/// 部门表
	/// </summary>
	[Serializable]
    [Table("T_Department")]
	public partial class T_Department
	{
		public T_Department()
		{}
		#region Model
	 
		/// <summary>
		/// 部门ID
		/// </summary>
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID
		{
            set;
            get;
		}
		/// <summary>
		/// 部门名称
		/// </summary>
		public string Name
		{
            set;
            get;
		}
		/// <summary>
		/// 部门代码
		/// </summary>
		public string Code
		{
            set;
            get;
		}
		/// <summary>
		/// 部门地址
		/// </summary>
		public string Address
		{
            set;
            get;
		}
        /// <summary>
        ///部门类型 1镇2县
		/// </summary>
        public int Department_Type
        {
            set;
            get;
        }
        /// <summary>
        ///县ID
		/// </summary>
        public int CountyId
        {
            set;
            get;
        } 
        /// <summary>
        ///镇ID
        /// </summary>
        public int TownInfoId
        {
            set;
            get;
        }

        /// <summary>
        ///是否删除
        /// </summary>
        public int  IsDelete
        {
            set;
            get;
        }

        /// <summary>
        ///操作人
        /// </summary>
        public int? UserID
        {
            set;
            get;
        }

        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime? OperationTime
        {
            set;
            get;
        }

        
		#endregion Model

	}
}

//Address	ghdfghdf1
//Code	YAHKGA
//CountyId	2
//Department_Type	1
//ID	15
//IsDelete	0
//Name	河口乡派出所
//OperationTime	
//TownInfoId	21
//UserID	