using System;

namespace Xunit.Ioc
{
    /// <summary>
    /// This attribute specifies the concrete <see cref="IDependencyResolverBootstrapper"/> class that
    /// the test runner will use to resolve the test class and its dependencies.
    /// </summary>
    /// <remarks>
    /// It can be placed on a test class or on the test assembly, or both. If specified on both, the
    /// class level attribute is preferred. The specified class must implement 
    /// <see cref="IDependencyResolverBootstrapper"/>, must have a public default constructor and must not
    /// be abstract.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false)]
    public class DependencyResolverBootstrapperAttribute : Attribute
    {
        /// <summary>
        /// Specifies the type of a concrete class that will be used to create the container.
        /// It will implement <see cref="IDependencyResolverBootstrapper"/>.
        /// </summary>
        public Type BootstrapperType { get; private set; }

        /// <param name="bootstrapperType">
        /// The type of a concrete class that will be used to create the container.
        /// It must implement <see cref="IDependencyResolverBootstrapper"/>.
        /// </param>
        public DependencyResolverBootstrapperAttribute(Type bootstrapperType)
        {
            BootstrapperType = bootstrapperType;
            if (typeof(IDependencyResolverBootstrapper).IsAssignableFrom(bootstrapperType) == false)
                throw new ArgumentException("Type must implement IDependencyResolverBootstrapper", "bootstrapperType");
        }
    }
}
