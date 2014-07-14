using System.Linq;
using System.Reflection;
using LightInject;

namespace Xunit.Ioc.LightInject
{
    /// <summary>
    /// This module allows you to easily register all your test classes within the container
    /// </summary>
    /// <remarks>
    /// It searches for all test classes with the <see cref="RunWithAttribute"/> set to use
    /// the <see cref="IocTestClassCommand"/> and automatically adds them to the container.
    /// </remarks>
    public static class LightInjectTestModule
    {
        public static void RegisterTestModules(this IServiceRegistry serviceRegistry, Assembly assembly)
        {
            var testClasses = from type in assembly.GetTypes()
                              where type.IsClass && type.IsAbstract == false && type.IsGenericTypeDefinition == false
                              let runWithAttr = type.GetCustomAttributes(typeof(RunWithAttribute), false)
                                  .Cast<RunWithAttribute>()
                                  .FirstOrDefault()
                              where runWithAttr != null && runWithAttr.TestClassCommand == typeof(IocTestClassCommand)
                              select type;


            foreach (var testClass in testClasses)
            {
                serviceRegistry.Register(testClass, new PerScopeLifetime());
            }
        }
    }
}
