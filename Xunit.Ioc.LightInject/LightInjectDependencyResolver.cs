using System;
using LightInject;

namespace Xunit.Ioc.LightInject
{
    public class LightInjectDependencyResolver : IDependencyResolver
    {
        private readonly ServiceContainer _container;

        public LightInjectDependencyResolver(ServiceContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
        }

        public object GetType(Type type)
        {
            return _container.GetInstance(type);
        }

        public IDependencyScope CreateScope()
        {
            _container.BeginScope();
            return this;
        }
    }
}
