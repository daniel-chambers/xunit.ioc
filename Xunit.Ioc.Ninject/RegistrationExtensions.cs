using System;
using Ninject.Syntax;
using Ninject.Extensions.NamedScope;

namespace Xunit.Ioc.Ninject
{
    /// <summary>
    /// Some utility extensions for Ninject component registration
    /// </summary>
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Registers a component so all dependant components will resolve the same shared instance within the test
        /// lifetime scope.
        /// </summary>
        public static IBindingNamedWithOrOnSyntax<T> InstancePerTest<T>(this IBindingInSyntax<T> binding)
        {
            if (binding == null)
                throw new ArgumentNullException("binding");

            return binding.InNamedScope(NinjectDependencyResolver.TestLifetimeScopeTag);
        }
    }
}
