using System;
using Ninject;

namespace Xunit.Ioc.Ninject
{
    /// <summary>
    /// Implements <see cref="IDependencyResolver"/> and wraps an Ninject <see cref="IKernel"/>.
    /// </summary>
    /// <remarks>
    /// Typically you'd use this to wrap your <see cref="IKernel"/> instance and return it
    /// from your <see cref="IDependencyResolverBootstrapper"/> class.
    /// </remarks>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// The tag used on the Ninject named scope that test classes represent.
        /// </summary>
        /// <remarks>
        /// You should consider using the 
        /// <see cref="RegistrationExtensions.InstancePerTest{T}"/>
        /// method instead of this tag directly.
        /// </remarks>
        public const string TestLifetimeScopeTag = "TestLifetime";

        /// <summary>
        /// Creates an <see cref="NinjectDependencyResolver"/>
        /// </summary>
        /// <param name="kernel">The <see cref="IKernel"/> to wrap</param>
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <inheritdoc />
        public IDependencyScope CreateScope()
        {
            return this;
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public object GetType(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
