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
            assembly.RegisterTestClasses(t => serviceRegistry.Register(t, new PerScopeLifetime()));
        }
    }
}
