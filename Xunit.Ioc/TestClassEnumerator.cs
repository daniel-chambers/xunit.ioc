using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Ioc
{
    /// <summary>
    /// This class provides extention methods to ease setting up your container. 
    /// </summary>
    /// <remarks>
    /// It searches for all test classes with the <see cref="RunWithAttribute"/> set to use
    /// the <see cref="IocTestClassCommand"/>.
    /// </remarks>
    static public class TestClassEnumerator
    {
        /// <param name="assemblies">The list of <see cref="Assembly"/> to scan</param>
        /// <returns>List of <see cref="Type"/>s found</returns>
        public static IEnumerable<Type> GetTestClasses(this IEnumerable<Assembly> assemblies)
        {
            return from assembly in assemblies
                from type in assembly.GetTypes()
                where type.IsClass && type.IsAbstract == false && type.IsGenericTypeDefinition == false
                let runWithAttr = type.GetCustomAttributes(typeof (RunWithAttribute), false)
                                      .Cast<RunWithAttribute>()
                                      .FirstOrDefault()
                where runWithAttr != null && runWithAttr.TestClassCommand == typeof (IocTestClassCommand)
                select type;
        }

        /// <param name="assembly">The <see cref="Assembly"/> to scan</param>
        /// <returns>List of <see cref="Type"/>s found</returns>
        public static IEnumerable<Type> GetTestClasses(this Assembly assembly)
        {
            var assemblies = new[] { assembly };
            return assemblies.GetTestClasses();
        }

        /// <param name="assemblies">The list of <see cref="Assembly"/>s to scan</param>
        /// <param name="registerAction">An action to execute for each <see cref="Type"/> found</param>
        public static void RegisterTestClasses(this IEnumerable<Assembly> assemblies, Action<Type> registerAction)
        {
            foreach (var type in assemblies.GetTestClasses())
            {
                registerAction(type);
            }
        }

        /// <param name="assembly">The <see cref="Assembly"/> to scan</param>
        /// <param name="registerAction">An action to execute for each <see cref="Type"/> found</param>
        public static void RegisterTestClasses(this Assembly assembly, Action<Type> registerAction)
        {
            foreach (var type in assembly.GetTestClasses())
            {
                registerAction(type);
            }
        }
    }
}
