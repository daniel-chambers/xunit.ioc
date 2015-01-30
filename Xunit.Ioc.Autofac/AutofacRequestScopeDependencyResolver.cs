using Autofac;
using Autofac.Core.Lifetime;

namespace Xunit.Ioc.Autofac
{
    /// <summary>
    /// Wraps an Autofac <see cref="ILifetimeScope"/> as an <see cref="IDependencyResolver"/>. 
    /// Scopes created by this class are tagged as a Request scope (suitable for use with
    /// InstancePerRequest) and wrapped in a parent scope that is tagged as the Test scope.
    /// </summary>
    /// <remarks>
    /// Typically you'd use this to wrap your <see cref="IContainer"/> instance and return it
    /// from your <see cref="IDependencyResolverBootstrapper"/> class.
    /// </remarks>
    public class AutofacRequestScopeDependencyResolver : AutofacDependencyScope, IDependencyResolver
    {
        /// <summary>
        /// Creates an <see cref="AutofacDependencyResolver"/>
        /// </summary>
        /// <param name="lifetimeScope">The <see cref="ILifetimeScope"/> to wrap</param>
        public AutofacRequestScopeDependencyResolver(ILifetimeScope lifetimeScope) 
            : base(lifetimeScope)
        {
        }

        /// <inheritdoc />
        public IDependencyScope CreateScope()
        {
            var testLifetime = LifetimeScope.BeginLifetimeScope(AutofacDependencyResolver.TestLifetimeScopeTag);
            var webRequestLifetime = testLifetime.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            return new NestedAutofacDependencyScope(testLifetime, webRequestLifetime);
        }
    }
}