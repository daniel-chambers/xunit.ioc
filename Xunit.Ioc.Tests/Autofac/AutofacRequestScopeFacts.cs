using Autofac;
using NSubstitute;
using Xunit.Ioc.Autofac;
using Xunit.Should;

namespace Xunit.Ioc.Tests.Autofac
{
    [RunWith(typeof(IocTestClassCommand))]
    [DependencyResolverBootstrapper(typeof(AutofacRequestScopeTestsBootstrapper))]
    public class AutofacRequestScopeFacts
    {
        private readonly IDependency _dependency;
        private readonly IOtherDependency _otherDependency;

        public AutofacRequestScopeFacts(IDependency dependency, IOtherDependency otherDependency)
        {
            _dependency = dependency;
            _otherDependency = otherDependency;
        }

        [Fact]
        public void DependencyIsInjectedWhenAFactAttributeIsUsed()
        {
            _dependency.ShouldNotBeNull();
        }

        [Fact]
        public void DependencyIsSharedOnAPerTestBasis()
        {
            _otherDependency.ShouldNotBeNull();
            _otherDependency.Dependency.ShouldBeSameAs(_dependency);
        }
    }

    public class AutofacRequestScopeTestsBootstrapper : IDependencyResolverBootstrapper
    {
        public static readonly IContainer Container;
        public static readonly IDependencyResolver DependencyResolver;

        static AutofacRequestScopeTestsBootstrapper()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(c => Substitute.For<IDependency>())
                .As<IDependency>()
                .InstancePerRequest(); //PerRequest instead of PerTest
            containerBuilder.RegisterType<OtherDependency>()
                .As<IOtherDependency>();
            containerBuilder.RegisterModule(new TestsModule(typeof(AutofacTestsBootstrapper).Assembly));

            Container = containerBuilder.Build();
            DependencyResolver = new AutofacRequestScopeDependencyResolver(Container);
        }

        public IDependencyResolver GetResolver()
        {
            return DependencyResolver;
        }
    }
}