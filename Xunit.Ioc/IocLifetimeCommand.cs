using System;
using System.Linq;
using Xunit.Sdk;

namespace Xunit.Ioc
{
    /// <summary>
    /// A <see cref="ITestCommand"/> that wraps any other <see cref="ITestCommand"/>
    /// and resolves the test class instance from the container.
    /// </summary>
    /// <remarks>
    /// These are manufactured by the <see cref="IocTestClassCommand"/>.
    /// </remarks>
    public class IocLifetimeCommand : TestCommand
    {
        private readonly ITestCommand _innerCommand;

        /// <param name="innerCommand">The <see cref="ITestCommand"/> to wrap</param>
        /// <param name="method">The method being used as a test</param>
        public IocLifetimeCommand(ITestCommand innerCommand, IMethodInfo method)
            : base(method, MethodUtility.GetDisplayName(method), MethodUtility.GetTimeoutParameter(method))
        {
            _innerCommand = innerCommand;
        }

        /// <inheritdoc/>
        public override bool ShouldCreateInstance
        {
            get { return false; } //We're creating the instance out of the container
        }

        /// <inheritdoc/>
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
