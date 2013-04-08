using Autofac;

namespace Xunit.Ioc.Autofac
{
    /// <summary>
    /// Implements <see cref="IDependencyResolver"/> and wraps an Autofac <see cref="ILifetimeScope"/>.
    /// </summary>
    /// <remarks>
    /// Typically you'd use this to wrap your <see cref="IContainer"/> instance and return it
    /// from your <see cref="IDependencyResolverBootstrapper"/> class.
    /// </remarks>
    public class AutofacDependencyResolver : AutofacDependencyScope, IDependencyResolver
    {
        /// <summary>
        /// The tag used on the <see cref="ILifetimeScope"/> that wraps tests.
        /// </summary>
        /// <remarks>
        /// You should consider using the 
        /// <see cref="RegistrationExtensions.InstancePerTest{TLimit,TActivatorData,TStyle}"/>
        /// method instead of this tag directly.
        /// </remarks>
        public const string TestLifetimeScopeTag = "TestLifetime";

        /// <summary>
        /// Creates an <see cref="AutofacDependencyResolver"/>
        /// </summary>
        /// <param name="lifetimeScope">The <see cref="ILifetimeScope"/> to wrap</param>
        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
            : base(lifetimeScope)
        {
        }

        /// <inheritdoc />
        public IDependencyScope CreateScope()
        {
            return new AutofacDependencyScope(LifetimeScope.BeginLifetimeScope(TestLifetimeScopeTag));
        }
    }
}
