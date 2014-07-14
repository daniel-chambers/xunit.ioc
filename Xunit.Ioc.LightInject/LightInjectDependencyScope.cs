using System;
using LightInject;

namespace Xunit.Ioc.LightInject
{
    public class LightInjectDependencyScope : IDependencyScope
    {
        private readonly Scope _scope;
        private readonly ServiceContainer _container;

        public LightInjectDependencyScope(ServiceContainer container, Scope scope)
        {
            _container = container;
            _scope = scope;
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public object GetType(Type type)
        {
            if (_container.ScopeManagerProvider.GetScopeManager().CurrentScope != _scope)
            {
                throw new InvalidOperationException("Attempt to create scope intance withing the wrong scope");
            }
            return _container.GetInstance(type);
        }
    }
}