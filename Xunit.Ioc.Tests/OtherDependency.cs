using System;

namespace Xunit.Ioc.Tests
{
    public class OtherDependency : IDisposable, IOtherDependency
    {
        public IDependency Dependency { get; private set; }

        public OtherDependency(IDependency dependency)
        {
            Dependency = dependency;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}