using swg.Core.Autofac;
using swg.Core.Services;
using swg.Core.Stub;
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
        }

        protected void Application_End() {
            var logger = OperationLoggerStub.GetInstance() as IDisposable;
            logger?.Dispose();
        }

    }
}
