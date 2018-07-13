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
            builder.RegisterType<OperationStorage>().As<IOperationStorage>().SingleInstance();
            builder.RegisterType<ResultStorageStub>().As<IResultStorage>().SingleInstance();
            var logger = OperationLoggerStub.GetInstance();
            builder.RegisterInstance(logger).As<IOperationLogger>().SingleInstance().ExternallyOwned();
            builder.Register(c => OperationStorage.GetInstance(c)).As<IOperationStorage>().SingleInstance();

            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            return resolver;
        }
    }
}