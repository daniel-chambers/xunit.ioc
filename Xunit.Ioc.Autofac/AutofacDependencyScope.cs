using System;
using Autofac;

namespace Xunit.Ioc.Autofac
{
    /// <summary>
    /// Implements <see cref="IDependencyScope"/> and wraps an Autofac <see cref="ILifetimeScope"/>
    /// </summary>
    public class AutofacDependencyScope : IDependencyScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// The <see cref="ILifetimeScope"/> wrapped by this instance
        /// </summary>
        protected ILifetimeScope LifetimeScope { get { return _lifetimeScope; } }

        /// <summary>
        /// Creates an <see cref="AutofacDependencyScope"/>
        /// </summary>
        /// <param name="lifetimeScope">The <see cref="ILifetimeScope"/> to wrap</param>
        public AutofacDependencyScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        /// <inheritdoc />
        public object GetType(Type type)
        {
            return _lifetimeScope.Resolve(type);
        }
    }
}
