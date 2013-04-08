using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Xunit.Ioc
{
    /// <summary>
    /// This test class command enables the test runner to resolve instances of the test classes
    /// from an IoC container. 
    /// </summary>
    /// <remarks>
    /// Specify this class with the <see cref="RunWithAttribute"/> at test class level in order to use it 
    /// on your tests. To specify which container to resolve the test classes from, use the 
    /// <see cref="DependencyResolverBootstrapperAttribute"/>
    /// </remarks>
    /// <seealso cref="DependencyResolverBootstrapperAttribute"/>
    public class IocTestClassCommand : ITestClassCommand
    {
        private readonly TestClassCommand _testClassCommand;

        /// <summary>
        /// Creates an instace of the <see cref="IocTestClassCommand"/>.
        /// </summary>
        public IocTestClassCommand()
        {
            _testClassCommand = new TestClassCommand();
        }

        /// <inheritdoc/>
        public int ChooseNextTest(ICollection<IMethodInfo> testsLeftToRun)
        {
            return _testClassCommand.ChooseNextTest(testsLeftToRun);
        }

        /// <inheritdoc/>
        public Exception ClassFinish()
        {
            return _testClassCommand.ClassFinish();
        }

        /// <inheritdoc/>
        public Exception ClassStart()
        {
            return _testClassCommand.ClassStart();
        }

        /// <inheritdoc/>
        public IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            return _testClassCommand.EnumerateTestCommands(testMethod)
                .Select(c => (ITestCommand)new IocLifetimeCommand(c, testMethod));
        }

        /// <inheritdoc/>
        public IEnumerable<IMethodInfo> EnumerateTestMethods()
        {
            return _testClassCommand.EnumerateTestMethods();
        }

        /// <inheritdoc/>
        public bool IsTestMethod(IMethodInfo testMethod)
        {
            return _testClassCommand.IsTestMethod(testMethod);
        }

        /// <inheritdoc/>
        public object ObjectUnderTest { get { return null; } }

        /// <inheritdoc/>
        public ITypeInfo TypeUnderTest
        {
            get { return _testClassCommand.TypeUnderTest; }
            set { _testClassCommand.TypeUnderTest = value; }
        }
    }
}