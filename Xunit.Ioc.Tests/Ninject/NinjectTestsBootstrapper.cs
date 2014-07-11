using Ninject;
using NSubstitute;
using Xunit.Ioc.Ninject;

namespace Xunit.Ioc.Tests.Ninject
{
    public class NinjectTestsBootstrapper : IDependencyResolverBootstrapper
    {
        public static readonly IDependencyResolver DependencyResolver;

        static NinjectTestsBootstrapper()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IDependency>().ToMethod<IDependency>(context => Substitute.For<IDependency>()).InstancePerTest();

            kernel.Bind<IOtherDependency>().To<OtherDependency>();

            kernel.Bind<IOtherDependencyFactory>().To<OtherDependencyFactory>();
            
            kernel.Load(new NinjectTestsModule(typeof(NinjectTestsBootstrapper).Assembly));

            DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        public IDependencyResolver GetResolver()
        {
            return DependencyResolver;
        }
    }
}