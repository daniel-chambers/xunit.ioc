using Autofac;

namespace Xunit.Ioc.Autofac
{
    public class AutofacDependencyResolver : AutofacDependencyScope, IDependencyResolver
    {
        public const string TestLifetimeScopeTag = "TestLifetime";

        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {
        }

        public IDependencyScope CreateScope()
        {
            return new AutofacDependencyScope(LifetimeScope.BeginLifetimeScope(TestLifetimeScopeTag));
        }
    }
}