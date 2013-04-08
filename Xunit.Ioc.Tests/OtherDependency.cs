namespace Xunit.Ioc.Tests
{
    public class OtherDependency : IOtherDependency
    {
        public IDependency Dependency { get; private set; }

        public OtherDependency(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}