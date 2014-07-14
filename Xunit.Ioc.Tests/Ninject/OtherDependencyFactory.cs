using Ninject;
using Ninject.Syntax;

namespace Xunit.Ioc.Tests.Ninject
{
    public class OtherDependencyFactory : IOtherDependencyFactory
    {
        private readonly IResolutionRoot _resolver;

        public OtherDependencyFactory(IResolutionRoot resolver)
        {
            _resolver = resolver;
        }

        public IOtherDependency Create()
        {
            return _resolver.Get<IOtherDependency>();
        }
    }
}
