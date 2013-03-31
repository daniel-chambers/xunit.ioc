using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Xunit.Ioc
{
    public class IocTestClassCommand : ITestClassCommand
    {
        private readonly TestClassCommand _testClassCommand;

        public IocTestClassCommand()
        {
            _testClassCommand = new TestClassCommand();
        }

        public int ChooseNextTest(ICollection<IMethodInfo> testsLeftToRun)
        {
            return _testClassCommand.ChooseNextTest(testsLeftToRun);
        }

        public Exception ClassFinish()
        {
            return _testClassCommand.ClassFinish();
        }

        public Exception ClassStart()
        {
            return _testClassCommand.ClassStart();
        }

        public IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            return _testClassCommand.EnumerateTestCommands(testMethod)
                .Select(c => (ITestCommand)new IocLifetimeCommand(c, testMethod));
        }

        public IEnumerable<IMethodInfo> EnumerateTestMethods()
        {
            return _testClassCommand.EnumerateTestMethods();
        }

        public bool IsTestMethod(IMethodInfo testMethod)
        {
            return _testClassCommand.IsTestMethod(testMethod);
        }

        public object ObjectUnderTest { get { return null; } }

        public ITypeInfo TypeUnderTest
        {
            get { return _testClassCommand.TypeUnderTest; }
            set { _testClassCommand.TypeUnderTest = value; }
        }
    }
}