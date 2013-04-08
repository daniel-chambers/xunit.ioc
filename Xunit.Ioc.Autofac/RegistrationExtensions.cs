using System;
using Autofac.Builder;

namespace Xunit.Ioc.Autofac
{
    /// <summary>
    /// Some utility extensions for Autofac component registration
    /// </summary>
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Registers a component so all dependant components will resolve the same shared instance within the test
        /// lifetime scope.
        /// </summary>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTest<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
        {
            if (registration == null)
                throw new ArgumentNullException("registration");

            return registration.InstancePerMatchingLifetimeScope(new object[]
            {
                AutofacDependencyResolver.TestLifetimeScopeTag
            });
        }
    }
}
