using System;
using System.Linq;
using Autofac;

namespace Xunit.Ioc.Autofac
{
    /// <summary>
    /// Wraps multiple Autofac <see cref="ILifetimeScope"/>s, disposes them all when disposed,
    /// and resolves from the innermost one.
    /// </summary>
    /// <remarks>
    /// This class allows you to created nested scopes and treat them as a single scope. Disposal
    /// is done in from innermost to outermost order and resolution is from the innermost scope.
    /// </remarks>
    public class NestedAutofacDependencyScope : IDependencyScope
    {
        private readonly ILifetimeScope[] _lifetimeScopes;

        /// <summary>
        /// Creates an <see cref="NestedAutofacDependencyScope"/>
        /// </summary>
        /// <param name="lifetimeScopes">The nested scopes, in outermost to innermost order.</param>
        public NestedAutofacDependencyScope(params ILifetimeScope[] lifetimeScopes)
        {
            if (lifetimeScopes == null)
                throw new ArgumentNullException("lifetimeScopes");
            if (lifetimeScopes.Length == 0)
                throw new ArgumentException("lifetimeScopes cannot be empty", "lifetimeScopes");

            _lifetimeScopes = lifetimeScopes;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var lifetimeScope in _lifetimeScopes.Reverse())
            {
                lifetimeScope.Dispose();
            }
        }

        /// <inheritdoc />
        public object GetType(Type type)
        {
            return _lifetimeScopes.Last().Resolve(type);
        }
    }
}