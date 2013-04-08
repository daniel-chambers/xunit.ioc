namespace Xunit.Ioc
{
    public interface IDependencyResolver : IDependencyScope
    {
        IDependencyScope CreateScope();
    }
}