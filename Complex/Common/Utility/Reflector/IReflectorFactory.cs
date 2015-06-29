using System.Reflection;

namespace Complex.Common.Utility.Reflector
{
    /// <summary>
    /// 反射器工厂
    /// </summary>
    public interface IReflectorFactory
    {
        IMemberAccessor GetFieldAccessor(FieldInfo field);
        IMemberAccessor GetPropertyAccessor(PropertyInfo property);
        ILMethodInvoker GetMethodInvoker(MethodBase method);
    }


}
