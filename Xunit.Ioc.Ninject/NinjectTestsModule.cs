using System.Collections.Generic;
using System.Reflection;
using Ninject.Modules;
using Ninject.Extensions.NamedScope;

namespace Xunit.Ioc.Ninject
{
    /// <summary>
    /// This module allows you to easily register all your test classes within the kernel
    /// </summary>
    /// <remarks>
    /// It searches for all test classes with the <see cref="RunWithAttribute"/> set to use
    /// the <see cref="IocTestClassCommand"/> and automatically adds them to the kernel.
    /// </remarks>
    public class NinjectTestsModule : NinjectModule
    {
        private readonly IEnumerable<Assembly> _assemblies;

        /// <param name="assembly">The assembly to search for test classes</param>
        public NinjectTestsModule(Assembly assembly)
            : this(new[] { assembly })
        {
        }

        /// <param name="assemblies">The assemblies to search for test classes</param>
        public NinjectTestsModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        /// <inheritdoc/>
        public override void Load()
        {
            _assemblies.RegisterTestClasses(
                t => Bind(t).ToSelf().DefinesNamedScope(NinjectDependencyResolver.TestLifetimeScopeTag));
        }
    }
}
