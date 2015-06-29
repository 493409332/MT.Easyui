/*
Author      : 张智
Date        : 2011-3-7
Description : 表示文件上传后的结果
*/


namespace Complex.Common.Utility
{
    /// <summary>
    /// 表示文件上传后的结果
    /// </summary>
    public enum UploadResult : byte
    {
        /// <summary>
        /// 上传成功
        /// </summary>
        [Description("上传成功")]
        Succed = 1,
        /// <summary>
        /// 文件为空
        /// </summary>
        [Description("文件为空")]
        FileEmpty,
        /// <summary>
        /// 文件过大
        /// </summary>
        [Description("文件过大")]
        FileOverflow,
        /// <summary>
        /// 文件过小
        /// </summary>
        [Description("文件过小")]
        FileShort,
        /// <summary>
        /// 文件类型(扩展名)是不允许的
        /// </summary>
        [Description("文件类型(扩展名)是不允许的")]
        TypeNotAllow
    }
}
