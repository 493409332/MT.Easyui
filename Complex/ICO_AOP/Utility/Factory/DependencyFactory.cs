using Complex.Common.Utility;
using Microsoft.Practices.Unity;
using MtAop.Factory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Complex.ICO_AOP.Attribute;
 

namespace Complex.ICO_AOP.Utility.Factory
{
    //创建一个容器的单例
    public static class DependencyUnityContainer
    {
        private static IUnityContainer _current;
        private static readonly object Locker = new object();
        public static IUnityContainer Current
        {
            get
            {
                if (_current == null)
                {
                    lock (Locker)
                    {
                        if (_current == null)
                        {
                            _current = new UnityContainer();
                        }
                    }
                }
                return _current;
            }
        }
    }
    //创建注册工厂
    public static class DependencyFactory
    {
        public static void RegisterDependency()
        {
//            //所有DLL路径
//            var dllPaths =
//                Directory.GetFiles(HttpContext.Current.Server.MapPath("/bin"), "*.dll");
//            //所有类型
//            var typeList =
//                dllPaths.Where(p => p.Contains("Complex.dll")).Select(path =>
//                {
//                    try
//                    {
//                        return Assembly.LoadFrom(path);
//                    }
//                    catch (Exception exp)
//                    {
//#if DEBUG
//                        throw exp;
//#endif
//                        //   Logger.Error(string.Format("IOC注册时，加载程序集[{0}]出错！", path), exp);
//                        return (Assembly)null;
//                    }
//                }

//                    ).Where(a => a != null).Select(a =>
//                    {
//                        try
//                        {
//                            return a.GetTypes();
//                        }
//                        catch (Exception exp)
//                        {
//#if DEBUG
//                            throw exp;
//#endif
//                            //  Logger.Error(string.Format("IOC注册时，获取程序集[{0}]内所有类型时出错！", a.GetName().Name), exp);
//                            return new Type[] { };
//                        }
//                    })
//                    .SelectMany(ts => ts).Where(t => t.Namespace != null && t.Namespace.Contains("Complex.Logical.Realization") && t.IsInterface == false && t.IsAbstract == false);

          

           

            var typeList =
                Assembly.LoadFrom(HttpContext.Current.Server.MapPath("/bin/") + "Complex.dll").GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("Realization") && t.IsInterface == false && t.IsAbstract == false);   
            //所有ITransientLifetimeManagerRegister类型    

            var ITransientLifetimeManagerRegisterls =
                typeList.Where(
                    t => t.FindInterfaces((tt, o) => o.Equals(tt), typeof(ITransientLifetimeManagerRegister)).Length > 0);   
            //所有IContainerControlledLifetimeManagerRegister类型
            var IContainerControlledLifetimeManagerRegisterls =
                typeList.Where(
                    t => t.FindInterfaces((tt, o) => o.Equals(tt), typeof(IContainerControlledLifetimeManagerRegister)).Length > 0);
            //所有IHierarchicalLifetimeManagerRegister类型
            var IHierarchicalLifetimeManagerRegisterls =
                typeList.Where(
                    t => t.FindInterfaces((tt, o) => o.Equals(tt), typeof(IHierarchicalLifetimeManagerRegister)).Length > 0);
            //所有IExternallyControlledLifetimeManagerRegister类型
            var IExternallyControlledLifetimeManagerRegisterls =
                typeList.Where(
                    t => t.FindInterfaces((tt, o) => o.Equals(tt), typeof(IExternallyControlledLifetimeManagerRegister)).Length > 0);
            //所有IPerThreadLifetimeManagerRegister类型
            var IPerThreadLifetimeManagerRegisterls =
                typeList.Where(
                    t => t.FindInterfaces((tt, o) => o.Equals(tt), typeof(IPerThreadLifetimeManagerRegister)).Length > 0);
            //所有IPerResolveLifetimeManagerRegister类型
            var IPerResolveLifetimeManagerRegisterls =
                typeList.Where(
                    t => t.FindInterfaces((tt, o) => o.Equals(tt), typeof(IPerResolveLifetimeManagerRegister)).Length > 0);

            //注册ITransientLifetimeManagerRegister类型
            foreach (var t in ITransientLifetimeManagerRegisterls)
            {
                var implementInterfaceList = t.FindInterfaces((tt, o) => !o.Equals(tt) && tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false)!=null && ((ICO_AOPEnableAttribute)tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false)).ICOEnable, typeof(ITransientLifetimeManagerRegister));
                foreach (var iType in implementInterfaceList)
                {

                    ICOConfigAttribute ds = (ICOConfigAttribute) t.GetCustomAttribute(typeof(ICOConfigAttribute), false);
                    ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);

                    if ( ia.AOPEnable )
                    { 
                         
                        var generator = new DynamicProxyGenerator(t, iType);
                        Type type = generator.GenerateType();
                        DependencyUnityContainer.Current.RegisterType(iType, type, ds.Description, new TransientLifetimeManager());
                    }
                    else
                    { 
                        DependencyUnityContainer.Current.RegisterType(iType, t, ds.Description, new TransientLifetimeManager());
                    }
                  
            
                  
                   
                }
            } 
            //注册IContainerControlledLifetimeManagerRegister类型
            foreach (var t in IContainerControlledLifetimeManagerRegisterls)
            {
                var implementInterfaceList = t.FindInterfaces((tt, o) => !o.Equals(tt) && tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) != null && ( (ICO_AOPEnableAttribute) tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) ).ICOEnable, typeof(IContainerControlledLifetimeManagerRegister));
                foreach (var iType in implementInterfaceList)
                {
                    ICOConfigAttribute ds = (ICOConfigAttribute) t.GetCustomAttribute(typeof(ICOConfigAttribute), false);
                    ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);

                    if ( ia.AOPEnable )
                    {
                        var generator = new DynamicProxyGenerator(t, iType);
                        Type type = generator.GenerateType();
                        DependencyUnityContainer.Current.RegisterType(iType, type, ds.Description, new TransientLifetimeManager());
                    }
                    else
                    {
                        
                        DependencyUnityContainer.Current.RegisterType(iType, t, ds.Description, new TransientLifetimeManager());
                    }
                }
            }

            //注册IHierarchicalLifetimeManagerRegister类型
            foreach (var t in IHierarchicalLifetimeManagerRegisterls)
            {
                var implementInterfaceList = t.FindInterfaces((tt, o) => !o.Equals(tt) && tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) != null && ( (ICO_AOPEnableAttribute) tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) ).ICOEnable, typeof(IHierarchicalLifetimeManagerRegister));
                foreach (var iType in implementInterfaceList)
                {
                    ICOConfigAttribute ds = (ICOConfigAttribute) t.GetCustomAttribute(typeof(ICOConfigAttribute), false);
                    ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);

                    if ( ia.AOPEnable )
                    {
                        var generator = new DynamicProxyGenerator(t, iType);
                        Type type = generator.GenerateType();
                        DependencyUnityContainer.Current.RegisterType(iType, type, ds.Description, new TransientLifetimeManager());
                    }
                    else
                    {
                        
                        DependencyUnityContainer.Current.RegisterType(iType, t, ds.Description, new TransientLifetimeManager());
                    }
                }
            }

            //注册IExternallyControlledLifetimeManagerRegister类型
            foreach (var t in IExternallyControlledLifetimeManagerRegisterls)
            {
                var implementInterfaceList = t.FindInterfaces((tt, o) => !o.Equals(tt) && tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) != null && ( (ICO_AOPEnableAttribute) tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) ).ICOEnable, typeof(IExternallyControlledLifetimeManagerRegister));
                foreach (var iType in implementInterfaceList)
                {
                    ICOConfigAttribute ds = (ICOConfigAttribute) t.GetCustomAttribute(typeof(ICOConfigAttribute), false);
                    ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);

                    if ( ia.AOPEnable )
                    {
                        var generator = new DynamicProxyGenerator(t, iType);
                        Type type = generator.GenerateType();
                        DependencyUnityContainer.Current.RegisterType(iType, type, ds.Description, new TransientLifetimeManager());
                    }
                    else
                    {
                       
                        DependencyUnityContainer.Current.RegisterType(iType, t, ds.Description, new TransientLifetimeManager());
                    }
                }
            }
            //注册IPerThreadLifetimeManagerRegister类型
            foreach (var t in IPerThreadLifetimeManagerRegisterls)
            {
                var implementInterfaceList = t.FindInterfaces((tt, o) => !o.Equals(tt) && tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) != null && ( (ICO_AOPEnableAttribute) tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) ).ICOEnable, typeof(IPerThreadLifetimeManagerRegister));
                foreach (var iType in implementInterfaceList)
                {
                    ICOConfigAttribute ds = (ICOConfigAttribute) t.GetCustomAttribute(typeof(ICOConfigAttribute), false);
                    ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);

                    if ( ia.AOPEnable )
                    {
                        var generator = new DynamicProxyGenerator(t, iType);
                        Type type = generator.GenerateType();
                        DependencyUnityContainer.Current.RegisterType(iType, type, ds.Description, new TransientLifetimeManager());
                    }
                    else
                    {
                       
                        DependencyUnityContainer.Current.RegisterType(iType, t, ds.Description, new TransientLifetimeManager());
                    }
                }
            }
            //注册IPerResolveLifetimeManagerRegister类型
            foreach (var t in IPerResolveLifetimeManagerRegisterls)
            {
                var implementInterfaceList = t.FindInterfaces((tt, o) => !o.Equals(tt) && tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) != null && ( (ICO_AOPEnableAttribute) tt.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false) ).ICOEnable, typeof(IPerResolveLifetimeManagerRegister));
                foreach (var iType in implementInterfaceList)
                {
                    ICOConfigAttribute ds = (ICOConfigAttribute) t.GetCustomAttribute(typeof(ICOConfigAttribute), false);
                    ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);

                    if ( ia.AOPEnable )
                    {
                        var generator = new DynamicProxyGenerator(t, iType);
                        Type type = generator.GenerateType();
                        DependencyUnityContainer.Current.RegisterType(iType, type, ds.Description, new TransientLifetimeManager());
                    }
                    else
                    {
                       
                        DependencyUnityContainer.Current.RegisterType(iType, t, ds.Description, new TransientLifetimeManager());
                    }
                }
            }


        }
    }
    //MVC 工厂注册方式
    public class DependencyMvcControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        "没有找到路由{0}",
                        requestContext.HttpContext.Request.Path));
            }
            if (!typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        "{0}该类型没有继承ControllerBase",
                        controllerType),
                    "controllerType");
            }
            try
            {
                //string filepath = HttpContext.Current.Server.MapPath("/InfoLog.txt");

                //File.AppendAllText(filepath, System.Environment.NewLine); 
                //File.AppendAllText(filepath, string.Format("*****   {0}   ************************* {1}", controllerType.Name, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                //foreach (var item in controllerType.GetProperties())
                //{
                //    File.AppendAllText(filepath, System.Environment.NewLine);
                //    File.AppendAllText(filepath, item.Name);
                //}

          
                return (IController)DependencyUnityContainer.Current.Resolve(controllerType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        "{0}创建该controller失败！",
                        controllerType),
                    ex);
            }
        }
    }
    //MVC 接口注册方式
    public class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
    /// <summary>
    /// WebApi 接口注册方式
    /// </summary>
    public class IoCContainer : System.Web.Http.Dependencies.IDependencyResolver
    {

        IUnityContainer container;
        public IoCContainer(IUnityContainer container)
        {
            this.container = container;
        }
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new ScopeContainer(container);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }

            //string filepath = HttpContext.Current.Server.MapPath("~/InfoLog.txt");

            //File.AppendAllText(filepath, System.Environment.NewLine);
            //File.AppendAllText(filepath, string.Format("*****   {0}   ************{1}************* {2}", serviceType.Name, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "IoCContainer public object GetService(Type serviceType)"));
            //foreach (var item in serviceType.GetProperties())
            //{
            //    File.AppendAllText(filepath, System.Environment.NewLine);
            //    File.AppendAllText(filepath, item.Name);
            //}
          

            //if (container.IsRegistered(serviceType))
            //{
            //    return container.Resolve(serviceType);
            //}
            //else
            //{
            //    return null;
            //}
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch
            {
                return null;
            }
            //string filepath = HttpContext.Current.Server.MapPath("~/InfoLog.txt");

            //File.AppendAllText(filepath, System.Environment.NewLine);
            //File.AppendAllText(filepath, string.Format("*****   {0}   ************{1}************* {2}", serviceType.Name, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "IoCContainer public IEnumerable<object> GetServices(Type serviceType)"));
            //foreach (var item in serviceType.GetProperties())
            //{
            //    File.AppendAllText(filepath, System.Environment.NewLine);
            //    File.AppendAllText(filepath, item.Name);
            //}
          
            //if (container.IsRegistered(serviceType))
            //{
            //    return container.ResolveAll(serviceType);
            //}
            //else
            //{
            //    return new List<object>();
            //}
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }

    public class ScopeContainer : System.Web.Http.Dependencies.IDependencyScope
    {
        protected IUnityContainer container;
        public ScopeContainer(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }

            //string filepath = HttpContext.Current.Server.MapPath("~/InfoLog.txt");

            //File.AppendAllText(filepath, System.Environment.NewLine);
            //File.AppendAllText(filepath, string.Format("*****   {0}   ************{1}************* {2}", serviceType.Name, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "ScopeContainer public object GetService(Type serviceType)"));
            //foreach (var item in serviceType.GetProperties())
            //{
            //    File.AppendAllText(filepath, System.Environment.NewLine);
            //    File.AppendAllText(filepath, item.Name);
            //}

            //if (container.IsRegistered(serviceType))
            //{
            //    return container.Resolve(serviceType);
            //}
           
            //if (serviceType.Name=="Default1Controller")
            //{
            //     return container.Resolve(serviceType);
            //}
            //return null;

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch
            {
                return null;
            }
            //string filepath = HttpContext.Current.Server.MapPath("~/InfoLog.txt");

            //File.AppendAllText(filepath, System.Environment.NewLine);
            //File.AppendAllText(filepath, string.Format("*****   {0}   ************{1}************* {2}", serviceType.Name, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "ScopeContainer  public IEnumerable<object> GetServices(Type serviceType)"));
            //foreach (var item in serviceType.GetProperties())
            //{
            //    File.AppendAllText(filepath, System.Environment.NewLine);
            //    File.AppendAllText(filepath, item.Name);
            //}
          
            //if (container.IsRegistered(serviceType))
            //{
            //    return container.ResolveAll(serviceType);
            //}
            //else
            //{
            //    return new List<object>();
            //}
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }

}
