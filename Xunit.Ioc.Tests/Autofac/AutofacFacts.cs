using Autofac;
using NSubstitute;
using Xunit.Extensions;
using Xunit.Ioc.Autofac;
using Xunit.Should;

namespace Xunit.Ioc.Tests.Autofac
{
    [RunWith(typeof(IocTestClassCommand))]
    [DependencyResolverBootstrapper(typeof(AutofacTestsBootstrapper))]
    public class IocInjectedFacts
    {
        private readonly IDependency _dependency;
        private readonly IOtherDependency _otherDependency;

        public IocInjectedFacts(IDependency dependency, IOtherDependency otherDependency)
        {
            _dependency = dependency;
            _otherDependency = otherDependency;
        }

        [Fact]
        public void DependencyIsInjectedWhenAFactAttributeIsUsed()
        {
            _dependency.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(1)]
        public void DependencyIsInjectedWhenATheoryAttributeIsUsed(int param)
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

    public class AutofacTestsBootstrapper : IDependencyResolverBootstrapper
    {
        public static readonly IContainer Container;
        public static readonly IDependencyResolver DependencyResolver;

        static AutofacTestsBootstrapper()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(c => Substitute.For<IDependency>())
                .As<IDependency>()
                .InstancePerTest();
            containerBuilder.RegisterType<OtherDependency>()
                .As<IOtherDependency>();
            containerBuilder.RegisterModule(new TestsModule(typeof(AutofacTestsBootstrapper).Assembly));

            Container = containerBuilder.Build();
            DependencyResolver = new AutofacDependencyResolver(Container);
        }

        public IDependencyResolver GetResolver()
        {
            return DependencyResolver;
        }
    }
}