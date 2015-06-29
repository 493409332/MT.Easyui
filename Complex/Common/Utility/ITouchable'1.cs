/*
Author      : 张智
Date        : 2011-4-1
Description : 可以传递数据项的对象 泛型版本
*/


namespace Complex.Common.Utility
{
    /// <summary>
    /// 可以传递数据项的对象 泛型版本
    /// </summary>
    public interface ITouchable<T>
    {
        T Data { get; set; }
    }
}
