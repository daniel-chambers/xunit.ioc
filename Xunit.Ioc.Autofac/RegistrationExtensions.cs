using System;
using Autofac.Builder;

namespace Xunit.Ioc.Autofac
{
	public static class RegistrationExtensions
	{
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