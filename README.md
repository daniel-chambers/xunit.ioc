Xunit.Ioc
=========

Xunit.Ioc is an extension to XUnit that allows you to resolve test classes out of a dependency injection container. Autofac and Ninject are supported out of the box, but it's very easy to integrate it with your dependency injection container (and we accept pull requests!).

Why is this useful? Being able to resolve your test classes out of your container makes writing integration tests easy, as you can use the exact same container you use to compose your application normally to compose it for your integration tests. As the test class will be resolved out of your container, you can get the components you want to test injected right into your test class's constructor.

Xunit.Ioc is available via NuGet. 
* Autofac support: `Install-Package xunit.ioc.autofac` 
* Ninject support: `Install-Package xunit.ioc.ninject`
* Any other container: `Install-Package xunit.ioc` (you will need to write the integration components for the other container yourself)

**Note:** If you're using the [XUnit.net Contrib ReSharper plugin](https://xunitcontrib.codeplex.com/) to run your tests, please make sure you have at least v1.0 installed, as it only supports tests using XUnit's `RunWith` attribute since v1.0.

To use Xunit.Ioc, there are a couple of steps to perform to set things up (these examples will use the Autofac integration). First, place an Xunit `RunWith` attribute on your test class:

```c#
[RunWith(typeof(IocTestClassCommand))]
public class MyComponentFacts
{
    ...
}
```

Now you need to tell Xunit.Ioc where to get your container from by placing the `DependencyResolverBootstrapper` attribute specifying your container bootstrapper on the test class (you can also put this attribute on the assembly instead to have it apply to all classes):
```c#
[RunWith(typeof(IocTestClassCommand))]
[DependencyResolverBootstrapper(typeof(AutofacContainerBootstrapper))]
public class MyComponentFacts
{
    ...
}
```

The `AutofacContainerBootstrapper` class specified in the `DependencyResolverBootstrapper` attribute above is a class we'll need to create. It needs to implement `IDependencyResolverBootstrapper` in some way like the following (typically you could inherit from your application's existing bootstrapper class and implement the interface there):

```c#
public class AutofacContainerBootstrapper : IDependencyResolverBootstrapper
{
    private static readonly object _lock = new object();
    private static IDependencyResolver _dependencyResolver;

    public IContainer CreateContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<MyComponent>().As<IMyComponent>();
        
        builder.RegisterModule(new TestsModule(typeof(AutofacContainerBootstrapper).Assembly));
        
        return builder.Build();
    }

    public IDependencyResolver GetResolver()
    {
        lock (_lock)
        {
            if (_dependencyResolver == null)
                _dependencyResolver = new AutofacDependencyResolver(CreateContainer());
            return _dependencyResolver;
        }
    }
}
```

Note the registration of the `TestsModule` into the container above. This is a helpful Autofac module that will locate all your test classes (searching in the assembly passed into its constructor) that use Xunit.Ioc and will register them in the container.

Now that's done, you can sit back, have your components injected into your test class via the constructor, and write some tests like so:

```c#
[RunWith(typeof(IocTestClassCommand))]
[DependencyResolverBootstrapper(typeof(AutofacContainerBootstrapper))]
public class MyComponentFacts
{
    private readonly IMyComponent _myComponent;

    public MyComponentFacts(IMyComponent myComponent)
    {
        _myComponent = myComponent;
    }

    [Fact]
    public void ItDoesThatThingItsSupposedToDo()
    {
        var result = _myComponent.DoThing();
        Assert.True(result);
    }
}
```

If you'd like to see this in action, take a look at the tests in the code (in particular look at the AutofacFacts test class).

## Contributors
* [Daniel Chambers](https://github.com/daniel-chambers)
* [Peter Major](https://github.com/petermajor)