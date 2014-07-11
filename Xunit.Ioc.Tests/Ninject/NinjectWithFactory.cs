using Xunit.Should;

namespace Xunit.Ioc.Tests.Ninject
{
    [RunWith(typeof(IocTestClassCommand))]
    [DependencyResolverBootstrapper(typeof(NinjectTestsBootstrapper))]
    public class NinjectWithFactory
    {
        private readonly IDependency _dependency;
        private readonly IOtherDependencyFactory _otherDependencyFactory;

        public NinjectWithFactory(IDependency dependency, IOtherDependencyFactory otherDependencyFactory)
        {
            _dependency = dependency;
            _otherDependencyFactory = otherDependencyFactory;
        }

        [Fact]
        public void CanUseFactoryToCreateDependency()
        {
            var otherDependency = _otherDependencyFactory.Create();
            otherDependency.Dependency.ShouldBeSameAs(_dependency);
        }
    }
}