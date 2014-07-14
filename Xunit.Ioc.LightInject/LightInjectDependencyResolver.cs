using System;
using LightInject;

namespace Xunit.Ioc.LightInject
{
    /// <summary>
    /// Implements <see cref="IDependencyResolver"/> and wraps an LightInject <see cref="IServiceContainer"/>.
    /// </summary>
    /// <remarks>
    /// Typically you'd use this to wrap your <see cref="IServiceContainer"/> instance and return it
    /// from your <see cref="IDependencyResolverBootstrapper"/> class.
    /// </remarks>
    public class LightInjectDependencyResolver : IDependencyResolver
    {
        private readonly IServiceContainer _container;

        /// <summary>
        /// Creates an <see cref="LightInjectDependencyResolver"/>
        /// </summary>
        /// <param name="container">The <see cref="IServiceContainer"/> to wrap</param>
        public LightInjectDependencyResolver(IServiceContainer container)
        {
            _container = container;
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public object GetType(Type type)
        {
            return _container.GetInstance(type);
        }

        /// <inheritdoc />
        public IDependencyScope CreateScope()
        {
            return new LightInjectDependencyScope(_container, _container.BeginScope());
        }
    }
}
