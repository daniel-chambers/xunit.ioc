using System;
using Ninject;

namespace Xunit.Ioc.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public const string TestLifetimeScopeTag = "TestLifetime";

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
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
            return _kernel.Get(type);
        }
    }
}
