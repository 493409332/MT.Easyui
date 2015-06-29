using System.Reflection;

namespace Complex.Common.Utility.Reflector
{
    /// <summary>
    /// 通过 MSIL 实现的反射器工厂
    /// </summary>
    internal class ILReflectorFactory : IReflectorFactory
    {
        public IMemberAccessor GetFieldAccessor(FieldInfo field)
        {
            return new ILFieldAccessor(field);
        }
        public IMemberAccessor GetPropertyAccessor(PropertyInfo property)
        {
            return new ILPropertyAccessor(property);
        }

        public ILMethodInvoker GetMethodInvoker(MethodBase method)
        {
            return new ILMethodInvoker(method);
        }
    }
}
