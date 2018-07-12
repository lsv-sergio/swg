using Autofac;
using Autofac.Integration.Mvc;
using swg.Core.Creators;
using swg.Core.Services;
using swg.Core.Services.Stub;
using swg.Core.Stub;
using System.Linq;

namespace swg.Core.Autofac {
    public class AutofacConfig {
        public static AutofacDependencyResolver BuildContainer() {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterTypes(typeof(MvcApplication).Assembly.GetTypes().Where(x => x.BaseType == typeof(OperationCreator)).ToArray());
            builder.RegisterType<OperationService>().AsSelf().SingleInstance();
            builder.RegisterType<ResultStorageStub>().As<IResultStorage>().SingleInstance();
            builder.RegisterType<OperationLoggerStub>().As<IOperationLogger>().SingleInstance().OnRelease(t => t.Dispose());
            var container = builder.Build();
            return new AutofacDependencyResolver(container);
        }
    }
}