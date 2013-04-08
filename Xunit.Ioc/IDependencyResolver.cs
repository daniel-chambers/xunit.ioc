namespace Xunit.Ioc
{
    /// <summary>
    /// Abstraction over an IoC container component that can create IoC container scopes
    /// (<see cref="IDependencyScope"/>)
    /// </summary>
    public interface IDependencyResolver : IDependencyScope
    {
        /// <summary>
        /// Creates a nested <see cref="IDependencyScope"/>
        /// </summary>
        /// <returns>The new nested scope</returns>
        IDependencyScope CreateScope();
    }
}
