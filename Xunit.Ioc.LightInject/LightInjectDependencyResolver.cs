using System;
using LightInject;

namespace Xunit.Ioc.LightInject
{
    public class LightInjectDependencyResolver : IDependencyResolver
    {
        private Scope _currentScope;
        private readonly ServiceContainer _container;

        public LightInjectDependencyResolver(ServiceContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            if (_currentScope != null)
            {
                _currentScope.Dispose();
            }
        }

        public object GetType(Type type)
        {
            return _container.GetInstance(type);
        }

        public IDependencyScope CreateScope()
        {
            if (_currentScope != null)
            {
                _currentScope.Dispose();
            }

            _currentScope = _container.BeginScope();
            
            return this;
        }
    }
}
