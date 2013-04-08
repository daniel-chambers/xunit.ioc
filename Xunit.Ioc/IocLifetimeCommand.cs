using System;
using System.Linq;
using Xunit.Sdk;

namespace Xunit.Ioc
{
    public class IocLifetimeCommand : TestCommand
    {
        private readonly ITestCommand _innerCommand;

        public IocLifetimeCommand(ITestCommand innerCommand, IMethodInfo method)
            : base(method, MethodUtility.GetDisplayName(method), MethodUtility.GetTimeoutParameter(method))
        {
            _innerCommand = innerCommand;
        }

        public override bool ShouldCreateInstance
        {
            get { return false; } //We're creating the instance out of the container
        }

        public override MethodResult Execute(object testClass)
        {
            if (testClass != null)
                throw new InvalidOperationException("testClass is unexpectedly not null");

            var bootstrapper = GetContainer();
            using (var lifetimeScope = bootstrapper.CreateScope())
            {
                testClass = lifetimeScope.GetType(testMethod.Class.Type);
                return _innerCommand.Execute(testClass);
            }
        }

        private IDependencyResolver GetContainer()
        {
            var containerBootstrapperAttribute =
                testMethod.Class.Type
                    .GetCustomAttributes(typeof(DependencyResolverBootstrapperAttribute), false)
                    .Cast<DependencyResolverBootstrapperAttribute>()
                    .FirstOrDefault()
                ??
                testMethod.Class.Type.Assembly
                    .GetCustomAttributes(typeof(DependencyResolverBootstrapperAttribute), false)
                    .Cast<DependencyResolverBootstrapperAttribute>()
                    .FirstOrDefault();

            if (containerBootstrapperAttribute == null)
                throw new InvalidOperationException("Cannot find an DependencyResolverBootstrapperAttribute on either the test assembly or class");

            var bootstrapper = (IDependencyResolverBootstrapper)Activator.CreateInstance(containerBootstrapperAttribute.BootstrapperType);
            return bootstrapper.GetResolver();
        }
    }
}