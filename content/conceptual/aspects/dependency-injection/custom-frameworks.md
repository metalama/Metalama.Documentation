---
uid: dependency-injection-custom-frameworks
summary: "Learn how to create a custom dependency injection framework adapter to integrate Metalama with other DI containers."
level: 400
keywords: "dependency injection adapter, custom DI framework, service locator, IDependencyInjectionFramework, Metalama extensibility"
created-date: 2025-12-07
modified-date: 2025-12-07
---

# Creating a custom DI framework adapter

The default dependency injection implementation in `Metalama.Extensions.DependencyInjection` pulls dependencies from constructor parameters, which works well with `Microsoft.Extensions.DependencyInjection`. However, you might need a different approach for:

* **Legacy DI frameworks** that use attribute-based injection (like older versions of Unity, Ninject, or Autofac)
* **Service locator patterns** where dependencies are resolved from a global provider
* **Custom resolution logic** specific to your application

## Architecture overview

The <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework> interface is the main abstraction of a DI framework adapter. 

The default implementation of this interface, handling the `Microsoft.Extensions.DependencyInjection` injection pattern, is provided by the <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> class.

If you want to create a custom framework adapter, you can either implement <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework> or override <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework>.

## Implementing IDependencyInjectionFramework

For simple requirements, implement the <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework> interface directly.

To illustrate the process, we'll build a simple adapter that generates `[Import]` attributes on properties for use with an attribute-based DI container.

### Step 1. Implement the framework interface

Create a class that implements <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework>. This interface has three methods:

* <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework.CanHandleDependency*>: Determines whether the adapter can handle the given dependency.
* <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework.IntroduceDependency*>: Introduces a dependency from an aspect into the target type, called via the <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.IntroduceDependency*> extension method or indirectly via <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute>.
* <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework.TryImplementDependency*>: Implements a dependency on an existing field or property in target code (not aspect code) when <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute> is applied.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/ImportAttributeFramework.Framework.cs]

### Step 2. Register the adapter

Register your adapter using a fabric. To automatically register when a package is referenced, use <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric>. For project-specific registration, use <xref:Metalama.Framework.Fabrics.ProjectFabric>:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/ImportAttributeFramework.Fabric.cs]

### Step 3. Use the aspect

Now you can create aspects that use <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute> and they will use your custom framework:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/ImportAttributeFramework.Aspect.cs]

### Result (simple IDependencyInjectionFramework)

When you apply the aspect to a class:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/ImportAttributeFramework.cs diff name="Import Attribute Framework"]

## Extending DefaultDependencyInjectionFramework

If your DI framework also uses constructor injection but requires customizations to how dependencies are resolved, extend <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> and override the <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework.GetStrategy*> method to return custom strategy implementations.

### Example: Factory-based injection

This example shows a custom adapter for services requiring factory-based creation. Instead of injecting the service directly, it injects `IServiceFactory<T>` and calls `Create()` to instantiate the service.

First, define the factory interfaces:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFramework.Additional.cs]

### Step 1. Implement the framework

Extend <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> and override <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework.CanHandleDependency*> to handle only types implementing `IFactoredService`. Override <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework.GetStrategy*> to return a custom strategy:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFramework.Framework.cs]

### Step 2. Implement the strategy

The strategy extends <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionStrategy> and returns a custom pull strategy that changes how constructor parameters are generated:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFramework.EagerStrategy.cs]

The key customizations are:

* **`ParameterType`**: Returns `IServiceFactory<T>` instead of `T` directly, so the constructor receives the factory.
* **`GetAssignmentStatement`**: Generates `this._logger = factory.Create();` instead of direct assignment.

### Step 3. Register and use

Register the framework in a fabric:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFramework.Fabric.cs]

Use <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute> in an aspect:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFramework.Aspect.cs]

### Result (eager loading)

When applied to a class, the framework injects the factory and calls `Create()`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFramework.cs diff name="Factory Framework"]

### Lazy initialization

For lazy initialization where `Create()` is called on first access instead of in the constructor, you need to:

1. Check the <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute.IsLazy> property in your framework's <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework.GetStrategy*> method
2. Implement a custom strategy that introduces a property with a lazy getter

The lazy strategy is more complex because it needs to:

- Introduce a `Func<IServiceFactory<T>>` field to store the factory getter
- Introduce a cache field for the created instance
- Introduce a property that calls `factory().Create()` on first access

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFrameworkLazy.LazyStrategy.cs]

Then use the aspect with <xref:Metalama.Extensions.DependencyInjection.IntroduceDependencyAttribute.IsLazy> set to `true`:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFrameworkLazy.Aspect.cs]

#### Result (lazy loading)

When applied to a class, the framework defers `Create()` until first property access:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.DependencyInjection/FactoryFrameworkLazy.cs diff name="Factory Framework Lazy"]

## Framework priority

When multiple adapters are registered, Metalama selects one based on priority, where lower numbers indicate higher priority. Built-in frameworks use priorities 100–101, while user-registered frameworks default to priority 0 (highest priority). To set a specific priority, use <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionOptionsBuilder.RegisterFramework*> and supply a value for the `priority` parameter.

> [!div class="see-also"]
> <xref:dependency-injection>
> <xref:Metalama.Extensions.DependencyInjection.Implementation.IDependencyInjectionFramework>
> <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionFramework>
> <xref:Metalama.Extensions.DependencyInjection.Implementation.DefaultDependencyInjectionStrategy>
> <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*>
