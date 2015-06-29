
namespace Complex.Common.Cache
{
    /// <summary>
    /// 可控的缓存委托
    /// </summary>
    /// <param name="needCache">是否需要缓存</param>
    /// <returns></returns>
    public delegate object OrCache(out bool needCache);
}
