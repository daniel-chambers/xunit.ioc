using System;

namespace Xunit.Ioc
{
    /// <summary>
    /// Abstraction over an IoC container component that represents a scope that can be used
    /// to resolve components and manages their lifecycle.
    /// </summary>
    public interface IDependencyScope : IDisposable
    {
        /// <summary>
        /// Gets an instance of the specified type
        /// </summary>
        /// <param name="type">The type of object to get</param>
        /// <returns>The instance</returns>
        object GetType(Type type);
    }
}
