using System;

namespace Xunit.Ioc
{
	public interface IDependencyScope : IDisposable
	{
		object GetType(Type type);
	}
}