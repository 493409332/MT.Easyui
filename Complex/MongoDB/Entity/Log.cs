
using Complex.Common.Enumspace;
using MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Mongodb.Entity
{
    public class Log
    {
        /// <summary>
        /// 实体类
        /// </summary>
        public Document Model { get; set; }
        /// <summary>
        ///实体类类型
        /// </summary>
        public string ModelTypeStr { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        ///操作人ID
        /// </summary>
        public int UserID
        {
            set;
            get;
        }
        /// <summary>
        /// 影响记录行数
        /// </summary>
        public int SaveChangesint { get; set; }
        /// <summary>
        ///操作人名字
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///操作人权限
        /// </summary>
        public string PurviewName { get; set; }
    }

   // [TableName("sys_logs")]
    public class LogModel
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// 操作表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 操作表中文名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 主键值
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        public string SqlText { get; set; }

        /// <summary>
        /// 操作Ip
        /// </summary>
        public string OperationIp { get; set; }


        //[DbField(false)]
        //public IEnumerable<LogDetailModel> Details
        //{
        //    get
        //    {
        //        return LogDetailDal.Instance.GetBy(KeyId);
        //    }
        //}

        //public override string ToString()
        //{
        //    return JSONhelper.ToJson(this);
        //}
    }

}
