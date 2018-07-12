using swg.Core.Autofac;
using swg.Core.Services;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI;

namespace swg {
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(AutofacConfig.BuildContainer());
        
            var operationService = DependencyResolver.Current.GetService(typeof(OperationService)) as OperationService;
            operationService.Init(DependencyResolver.Current);
        }
        protected void Application_End() {
            //var a = DependencyResolver.Current.GetService(typeof(IOperationLogger)) as IDisposable;
        }
    }
}
