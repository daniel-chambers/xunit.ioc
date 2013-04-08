using NSubstitute;
using Xunit.Extensions;
using Xunit.Should;

namespace Xunit.Ioc.Tests
{
    [RunWith(typeof(IocTestClassCommand))]
    [DependencyResolverBootstrapper(typeof(IocInjectedFactsBootstrapper))]
    public class IocInjectedFacts
    {
        private readonly IDependency _dependency;

        public IocInjectedFacts(IDependency dependency)
        {
            _dependency = dependency;
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
    }

    public class IocInjectedFactsBootstrapper : IDependencyResolverBootstrapper
    {
        public static readonly IDependencyResolver DependencyResolver = Substitute.For<IDependencyResolver>();
        public static readonly IDependencyScope DependencyScope = Substitute.For<IDependencyScope>();
        public static readonly IDependency Dependency = Substitute.For<IDependency>();

        static IocInjectedFactsBootstrapper()
        {
            DependencyResolver.CreateScope().ReturnsForAnyArgs(DependencyScope);
            DependencyScope.GetType(typeof(IocInjectedFacts)).Returns(new IocInjectedFacts(Dependency));
        }

        public IDependencyResolver GetResolver()
        {
            return DependencyResolver;
        }
    }
}