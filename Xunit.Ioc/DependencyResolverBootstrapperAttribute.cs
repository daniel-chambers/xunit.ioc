using System;

namespace Xunit.Ioc
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false)]
    public class DependencyResolverBootstrapperAttribute : Attribute
    {
        public Type BootstrapperType { get; private set; }

        public DependencyResolverBootstrapperAttribute(Type bootstrapperType)
        {
            BootstrapperType = bootstrapperType;
            if (typeof(IDependencyResolverBootstrapper).IsAssignableFrom(bootstrapperType) == false)
                throw new ArgumentException("Type must implement IDependencyResolverBootstrapper", "bootstrapperType");
        }
    }
}