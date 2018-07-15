using swg.Core.Autofac;
using swg.Core.Creators;
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
            var resolver = DependencyResolver.Current;
            var operationStorage = resolver.GetService<IOperationStorage>() ;
            var currentAssembly = typeof(MvcApplication).Assembly;
            var creatorSelector = resolver.GetService<OperationCreatorSelector>();
            operationStorage.AddOperationCreators(creatorSelector?.Select(currentAssembly));
        }

        protected void Application_End() {
            var logger = OperationLoggerStub.GetInstance() as IDisposable;
            logger?.Dispose();
        }

    }
}
