using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace Xunit.Ioc
{
	public class IocInjectedFactAttribute : FactAttribute
	{
		protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
		{
			yield return new IocLifetimeCommand(method);
		}
	}
}
