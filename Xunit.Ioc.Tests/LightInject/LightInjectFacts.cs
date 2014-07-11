using LightInject;
using NSubstitute;
using Xunit.Extensions;
using Xunit.Ioc.LightInject;
using Xunit.Should;

namespace Xunit.Ioc.Tests.LightInject
{
    [RunWith(typeof (IocTestClassCommand))]
    [DependencyResolverBootstrapper(typeof(LightInjectTestsBootstrapper))]
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

    public class LightInjectTestsBootstrapper : IDependencyResolverBootstrapper
    {
        public static readonly IDependencyResolver DependencyResolver;

        static LightInjectTestsBootstrapper()
        {
            var container = new ServiceContainer();

            container.Register(c => Substitute.For<IDependency>(), new PerScopeLifetime());
            container.Register<IOtherDependency, OtherDependency>();

            container.RegisterTestModules(typeof(LightInjectTestsBootstrapper).Assembly);

            DependencyResolver = new LightInjectDependencyResolver(container);
        }

        public IDependencyResolver GetResolver()
        {
            return DependencyResolver;
        }
    }
}
