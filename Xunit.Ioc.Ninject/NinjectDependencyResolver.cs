using Ninject;

namespace Xunit.Ioc.Ninject
{
    using System;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public const string TestLifetimeScopeTag = "TestLifetime";

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope CreateScope()
        {
            return this;
        }

        public void Dispose()
        {
        }

        public object GetType(Type type)
        {
            return kernel.Get(type);
        }
    }
}
