using System.Web.UI;
using Utility;

namespace Complex.Common.Utility.Extensions
{
    /// <summary>
    /// 对 System.Web.UI.Control 的扩展
    /// </summary>
    public static class ControlExtension
    {
        /// <summary>
        /// 获得服务器控件的html
        /// </summary>
        /// <param name="control">控件实例</param>
        /// <returns></returns>
        public static string RenderAsString(this Control control)
        {
            return WebUtil.GetPartial(control); 
        }
    }
}
