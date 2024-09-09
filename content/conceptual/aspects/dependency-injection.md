---
uid: dependency-injection
level: 300
summary: "The document provides a detailed guide on injecting dependencies into aspects using the Metalama.Extensions.DependencyInjection project. It covers consuming dependencies, selecting a dependency injection framework, and implementing an adaptor for a new dependency injection framework."
keywords: "dependency injection, Metalama.Extensions.DependencyInjection, .NET Core, constructor parameter, custom attribute, IDependencyInjectionFramework, introduce dependency, ServiceLocator, dependency injection framework, ILogger"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Injecting dependencies into aspects

Many aspects require services injected from a dependency injection container. For example, a caching aspect may depend on the `IMemoryCache` service. If you use the [Microsoft.Extensions.DependencyInjection](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection) framework, your aspect should pull this service from the constructor. If the target type of the aspect does not already accept this service from the constructor, the aspect will need to append this parameter to the constructor.

However, the code pattern that must be implemented to pull any dependency depends on the dependency injection framework used by the project. As we have seen, the default .NET Core framework requires a constructor parameter, but other frameworks may use an `[Import]` or `[Inject]` custom attribute.

In some cases, as the author of the aspect, you may not know which dependency injection framework will be used for the classes to which your aspect will be applied.

This is where the <xref:Metalama.Extensions.DependencyInjection> project comes in. Thanks to this namespace, your aspect can consume and pull a dependency with a single custom attribute. The code pattern to pull the dependency is abstracted by the <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework> interface, which is chosen by the user project.

The <xref:Metalama.Extensions.DependencyInjection> namespace is open source and hosted on [GitHub](https://github.com/postsharp/Metalama.Framework.Extensions). It currently has implementations for the following dependency injection frameworks:

* <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> implements the default .NET Core pattern (see [Dependency injection in .NET](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection)).
* <xref:Metalama.Extensions.DependencyInjection.ServiceLocator.ServiceLocatorDependencyInjectionFramework> can be used by classes or projects that are not instantiated by a dependency injection framework thanks to a simple service locator pattern.

The <xref:Metalama.Extensions.DependencyInjection> project is designed to make implementing other dependency injection frameworks easy.

## Consuming dependencies from your aspect

To consume a dependency from an aspect:

1. Add the `Metalama.Extensions.DependencyInjection` package to your project.
2. Add a field or automatic property of the desired type in your aspect class.
3. Annotate this field or property with the <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute> custom attribute. The following attribute properties are available:

  * <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute.IsLazy> resolves the dependency upon first use instead of upon initialization, and
  * <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute.IsRequired> throws an exception if the dependency is not available.

4. Use this field or property from any template member of your aspect.

### Example: default dependency injection patterns

The following example uses [Microsoft.Extensions.Hosting](https://learn.microsoft.com/dotnet/core/extensions/generic-host), typical to .NET Core applications, to build an application and inject services. The `Program.Main` method builds the host, and the host then instantiates our `Worker` class. We add a `[Log]` aspect to this class. The `Log` aspect class has a field of type `IMessageWriter` marked with the <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute> custom attribute. As you can see in the transformed code, this field is introduced into the `Worker` class and pulled from the constructor.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection/LogDefaultFramework.cs name="Dependency Injection"]

### Example: ServiceLocator

The following example is similar to the previous one but uses the `ServiceLocator` pattern instead of pulling dependencies from the constructor.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection.ServiceLocator/LogServiceLocator.cs name="Service Locator"]

## Selecting a dependency injection framework

By default, Metalama generates code for the default .NET dependency injection framework implemented in the ``Microsoft.Extensions.DependencyInjection`` namespace (also called the .NET Core dependency injection framework).

If you want to select a different framework for a project, generally adding a reference to the package implementing this dependency framework is sufficient, e.g., `Metalama.Extensions.DependencyInjection.ServiceLocator`. These packages typically include a <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> that registers itself. This works well when the project has a single dependency injection framework.

When several dependency injection frameworks can handle a specified dependency, Metalama select the one with the lowest priority value among them. This selection strategy can be customized for the whole project or for specified namespaces or types.

 To customize the selection strategy of the dependency injection framework for a specific aspect and dependency:

1. Add a <xref:Metalama.Framework.Fabrics.ProjectFabric> or <xref:Metalama.Framework.Fabrics.NamespaceFabric> as described in <xref:fabrics-configuration>.
2. From the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> or <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> method, call the <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*?text=amender.Outgoing.ConfigureDependencyInjection> method. Supply the empty delegate `builder => {}` as an argument to this method.
3. From this delegate, do one of the following things:
    * Call the <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionOptionsBuilder.SetFrameworkPriority*> method, to change the priority of a given framework (lower values win), or
    * Set the <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionOptionsBuilder.Selector?text=builder.Selector> property to your own implementation of <xref:Metalama.Extensions.DependencyInjection.IDependencyInjectionFrameworkSelector> interface.

## Implementing an adaptor for a new dependency injection framework

If you need to support a dependency injection framework or pattern for which no ready-made implementation exists, you can implement an adapter yourself.

See [Metalama.Extensions.DependencyInjection.ServiceLocator on GitHub](https://github.com/postsharp/Metalama.Framework.Extensions/tree/master/src/Metalama.Extensions.DependencyInjection.ServiceLocator) for a working example.

The steps are as follows:

1. Create a class library project that targets `netstandard2.0`.
2. Add a reference to the `Metalama.Extensions.DependencyInjection` package.
3. Implement the <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework> interface in a new public class. It is easier to start from the <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> class. In this case, you must override the <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionStrategy> class. See the source code and the class documentation for details.
4. Optionally create a <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> that registers the framework by calling <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*?text=amender.Outgoing.ConfigureDependencyInjection>, then  <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionOptionsBuilder.RegisterFramework*?text=builder.RegisterFramework>.

### Example

The following example shows how to implement the correct code generation pattern for the `ILogger` service under .NET Core. Whereas normal services usually require a parameter of the same type of the constructor, the `ILogger` service requires a parameter of the generic type `ILogger<T>`, where `T` is the current type used as a category.

Our implementation of <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework> implements the <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework.CanHandleDependency*> method and returns `true` only when the dependency is of type `ILogger`. The only difference in the default implementation strategy is the parameter type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection/LogCustomFramework.cs name="Custom Adapter"]



