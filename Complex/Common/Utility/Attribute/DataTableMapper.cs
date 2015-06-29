namespace Complex.Common.Utility.Attribute
{
    /// <summary>
    /// 数据表映射
    /// </summary>
    public class DataTableMapperAttribute : System.Attribute
    {
        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName { get; set; }

        private bool _haveKeyField = true;
        /// <summary>
        /// 是否有主键字段
        /// </summary>
        public bool HaveKeyField
        {
            get { return _haveKeyField; }
            set { _haveKeyField = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableMapperAttribute()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        public DataTableMapperAttribute(string tableName)
            : this(tableName, true)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="_haveKeyField"></param>
        public DataTableMapperAttribute(string tableName, bool _haveKeyField)
        {
            this.TableName = tableName;
            this.HaveKeyField = _haveKeyField;
        }
    }
}
