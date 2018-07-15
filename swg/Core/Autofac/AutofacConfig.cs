using Autofac;
using Autofac.Integration.Mvc;
using swg.Core.Services;
using swg.Core.Services.Stub;
using swg.Core.Stub;

namespace swg.Core.Autofac {
    public class AutofacConfig {

        public static AutofacDependencyResolver BuildContainer() {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<OperationStorage>().As<IOperationStorage>().SingleInstance();
            builder.RegisterType<ResultStorageStub>().As<IResultStorage>().SingleInstance();
            builder.Register(c => OperationLoggerStub.GetInstance()).As<IOperationLogger>().SingleInstance().ExternallyOwned();
            builder.Register(c => OperationStorage.GetInstance()).As<IOperationStorage>().SingleInstance();
            builder.RegisterType<OperationCreatorSelector>().AsSelf();
            var container = builder.Build();
            return new AutofacDependencyResolver(container);
        }
    }
}