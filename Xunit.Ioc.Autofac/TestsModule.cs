using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Xunit.Ioc.Autofac
{
	public class TestsModule : Module
	{
		private readonly IEnumerable<Assembly> _assemblies;

		public TestsModule(Assembly assembly)
			: this(new[] { assembly })
		{
		}

		public TestsModule(IEnumerable<Assembly> assemblies)
		{
			_assemblies = assemblies;
		}

		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			var testClasses = from assembly in _assemblies
			                  from type in assembly.GetTypes()
			                  where type.IsClass && type.IsAbstract == false && type.IsGenericTypeDefinition == false
			                  from method in type.GetMethods()
			                  where method.GetCustomAttributes(typeof(IocInjectedFactAttribute), false)
				                  .Cast<IocInjectedFactAttribute>()
				                  .Any()
			                  select type;

			foreach (var testClass in testClasses)
				builder.RegisterType(testClass);
		}
	}
}