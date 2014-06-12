using Ninject;
using NSubstitute;
using Xunit.Extensions;
using Xunit.Ioc.Ninject;
using Xunit.Should;

namespace Xunit.Ioc.Tests.Ninject
{
    [RunWith(typeof(IocTestClassCommand))]
    [DependencyResolverBootstrapper(typeof(NinjectTestsBootstrapper))]
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

    public class NinjectTestsBootstrapper : IDependencyResolverBootstrapper
    {
        public static readonly IDependencyResolver DependencyResolver;

        static NinjectTestsBootstrapper()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IDependency>().ToMethod<IDependency>(context => Substitute.For<IDependency>()).InstancePerTest();

            kernel.Bind<IOtherDependency>().To<OtherDependency>();

            kernel.Load(new NinjectTestsModule(typeof(NinjectTestsBootstrapper).Assembly));

            DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        public IDependencyResolver GetResolver()
        {
            return DependencyResolver;
        }
    }
}