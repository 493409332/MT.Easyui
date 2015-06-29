using System;

namespace Complex.Common.Utility.Attribute
{
    /// <summary>
    /// 设置为非映射数据库字段
    /// </summary>
    public class NoFieldAttribute : System.Attribute
    {
    }

    /// <summary>
    /// 映射为主键
    /// </summary>
    public class KeyFieldAttribute : System.Attribute {
        /// <summary>
        /// 主键字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FieldType { get; set; }
    }
}
