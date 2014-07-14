using System;
using LightInject;

namespace Xunit.Ioc.LightInject
{
    /// <summary>
    /// Implements <see cref="IDependencyScope"/> and wraps an LightInject <see cref="Scope"/>
    /// </summary>
    public class LightInjectDependencyScope : IDependencyScope
    {
        private readonly Scope _scope;
        private readonly IServiceContainer _container;

        /// <summary>
        /// Creates an <see cref="LightInjectDependencyScope"/>
        /// </summary>
        /// <param name="container">The <see cref="IServiceContainer"/> to wrap</param>
        /// <param name="scope">The <see cref="Scope"/> to wrap</param>
        public LightInjectDependencyScope(IServiceContainer container, Scope scope)
        {
            _container = container;
            _scope = scope;
        }


        /// <inheritdoc />
        public void Dispose()
        {
            _scope.Dispose();
        }


        /// <inheritdoc />
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