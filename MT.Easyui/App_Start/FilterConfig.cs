using System.Web;
using System.Web.Mvc;
using Complex.Common.Utility.Attribute;

namespace MT.Easyui
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoginInterceptFilter());
        }
    }
}