using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    static public class TestClassEnumerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTestClasses(this Assembly assembly)
        {
            var assemblies = new[] { assembly };
            return assemblies.GetTestClasses();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="registerAction"></param>
        public static void RegisterTestClasses(this IEnumerable<Assembly> assemblies, Action<Type> registerAction)
        {
            foreach (var type in assemblies.GetTestClasses())
            {
                registerAction(type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="registerAction"></param>
        public static void RegisterTestClasses(this Assembly assembly, Action<Type> registerAction)
        {
            foreach (var type in assembly.GetTestClasses())
            {
                registerAction(type);
            }
        }
    }
}
