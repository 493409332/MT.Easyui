/*
Author      : 张智
Date        : 2011-4-1
Description : 可以传递数据项的对象
*/


namespace Complex.Common.Utility
{
    /// <summary>
    /// 可以传递数据项的对象
    /// </summary>
    public interface ITouchable
    {
        object Data { get; set; }
    }
}
