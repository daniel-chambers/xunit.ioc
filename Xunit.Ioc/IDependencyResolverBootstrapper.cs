namespace Xunit.Ioc
{
    /// <summary>
    /// Classes that implement this interface can be used with the 
    /// <see cref="DependencyResolverBootstrapperAttribute"/> in order to specify the bootstrapper
    /// class the test runner should use to get the container instance it will use to resolve
    /// test classes.
    /// </summary>
    /// <remarks>
    /// Typically you'd implement this interface on your bootstrapper class and return an
    /// <see cref="IDependencyResolver"/> that wraps your IoC container.
    /// </remarks>
    public interface IDependencyResolverBootstrapper
    {
        /// <summary>
        /// Gets the <see cref="IDependencyResolver"/> that will be used to resolve the test class
        /// </summary>
        /// <returns>The <see cref="IDependencyResolver"/></returns>
        IDependencyResolver GetResolver();
    }
}
