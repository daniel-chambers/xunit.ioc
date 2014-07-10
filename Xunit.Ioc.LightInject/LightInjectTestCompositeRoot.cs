using System.Linq;
using System.Reflection;
using LightInject;

namespace Xunit.Ioc.LightInject
{
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
