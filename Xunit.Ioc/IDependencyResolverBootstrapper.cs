namespace Xunit.Ioc
{
    public interface IDependencyResolverBootstrapper
    {
        IDependencyResolver GetResolver();
    }
}