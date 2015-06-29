using MtAop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtAop.BaseAttribute
{
    /// <summary>
    /// 切片特性
    /// </summary>
    public abstract class AspectAttribute : Attribute
    {

        public virtual InvokeContext Action(InvokeContext context) { return context; }
    } 
    /// <summary>
    /// 方法执行前
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class PreAspectAttribute : AspectAttribute
    {
    }
    /// <summary>
    /// 方法执行后
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class PostAspectAttribute : AspectAttribute
    {
    }
    /// <summary>
    /// 方法执行异常
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class ExceptionAspectAttribute : AspectAttribute
    {
    }
}
