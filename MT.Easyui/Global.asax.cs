using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Complex.ICO_AOP.Utility.Factory;

namespace MT.Easyui
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // RegisterDependency就是注册接口与实例的关系．
            // setCongrollerFactory则是用MyDependencyMvcControllerFactory替代默认Controller工厂 
            DependencyFactory.RegisterDependency();
            //ICO MVC注册 
            ControllerBuilder.Current.SetControllerFactory(new DependencyMvcControllerFactory());
            //ICO API注册
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(DependencyUnityContainer.Current);
        }
        //void Session_Start(object sender, EventArgs e)
        //{

        //    //TODO

        //}

        //void Session_End(object sender, EventArgs e)
        //{

        //}
    }
}