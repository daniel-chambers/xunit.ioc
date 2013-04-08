using System;
using Autofac;

namespace Xunit.Ioc.Autofac
{
    public class AutofacDependencyScope : IDependencyScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        protected ILifetimeScope LifetimeScope { get { return _lifetimeScope; } }

        public AutofacDependencyScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        public object GetType(Type type)
        {
            return _lifetimeScope.Resolve(type);
        }
    }
}