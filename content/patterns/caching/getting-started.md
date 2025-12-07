---
uid: caching-getting-started
summary: "The document provides a guide on how to use Metalama Caching to enhance application performance, with instructions for projects with and without dependency injection. It also discusses the importance of customizing the caching key."
keywords: "caching, Metalama Caching, application performance, CacheAttribute, ICachingService, CachingService, .NET Core, caching key, CachingService.Default"
created-date: 2024-04-25
modified-date: 2025-11-30
---

# Getting started with Metalama Caching

If you have a time-consuming method that consistently returns the same value when called with identical arguments, caching this method can significantly enhance your application's performance. With Metalama Caching, this process can be as straightforward as adding the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> attribute from the [Metalama.Patterns.Caching.Aspects](https://www.nuget.org/packages/Metalama.Patterns.Caching.Aspects/) package.

The <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect supports both synchronous and `async` methods. For `async` methods returning `Task<T>` or `ValueTask<T>`, the aspect automatically awaits the result and caches the unwrapped value.

Before you can use the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect, your projects require some setup. The approach depends on your project's architecture: with or without dependency injection.

> [!WARNING]
> The fallback strategy to generate the cache key of a parameter is to use the `ToString` method. However, the default implementation of the `ToString` method doesn't return a unique string for custom classes and structs (the default implementation of `ToString` for records is more likely to be correct). After completing the initial steps of this _getting started_ guide, provide a cache key implementation for all parameter types of a cached method. For details, see <xref:caching-keys>.

## With dependency injection

If your project is designed for the .NET Core dependency injection framework (`Microsoft.Extensions.DependencyInjection`), follow these steps:

1. Add the [Metalama.Patterns.Caching.Aspects](https://www.nuget.org/packages/Metalama.Patterns.Caching.Aspects/) package into your project.
2. In your application setup logic, while adding services to the <xref:Microsoft.Extensions.DependencyInjection.IServiceCollection>, include a call to the <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*> extension method. This action will add an instance of the <xref:Metalama.Patterns.Caching.ICachingService> interface, which is consumed by the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted/GettingStarted.Program.cs]

The <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect is now available to all objects instantiated by the dependency injection container.

> [!NOTE]
> If your project uses a different dependency injection framework, you may need to create an adapter for this framework. Then create an instance of the <xref:Metalama.Patterns.Caching.ICachingService> interface using the <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method.  For details about DI adapters, see <xref:dependency-injection>.

### Example: setting up caching with dependency injection

In this example, we demonstrate how to add caching to a self-hosted .NET Core application. This application consists of two services, the primary service called `MainService` and a hypothetical `CloudCalculator`, which performs complex and slow computations.

`Program.Main` calls the <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*> extension method. This action makes <xref:Metalama.Patterns.Caching.ICachingService> available to `CloudCalculator`, which can use the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect. Note how the <xref:Metalama.Patterns.Caching.ICachingService> interface is automatically pulled into the `CloudCalculator` class.

Finally, `MainService` calls `CloudCalculator` as usual. It calls the `CloudCalculator.Add` three times with the same parameters and displays the actual number of operations performed at the end.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted/GettingStarted.cs ]

## Without dependency injection

If your project doesn't use dependency injection, the global default instance of the <xref:Metalama.Patterns.Caching.ICachingService> is used. It's exposed as the <xref:Metalama.Patterns.Caching.CachingService.Default?text=CachingService.Default> property.

Follow these steps to configure your project:

1. Add the [Metalama.Patterns.Caching.Aspects](https://www.nuget.org/packages/Metalama.Patterns.Caching.Aspects/) package into your project.
2. Create a file named, for instance, `CachingConfiguration.cs`, and add the following code:

    [!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted_NoDI/GettingStarted_NoDI.CachingConfiguration.cs ]

    This code disables dependency injection for the whole project. You can also add this attribute to individual types to disable dependency injection specifically for these types. You can also configure your project using fabrics and use the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*?text=amender.Outgoing.ConfigureCaching> method and modify the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingOptionsBuilder.UseDependencyInjection> property for selected namespaces or types.

3. Initialize the <xref:Metalama.Patterns.Caching.CachingService.Default?text=CachingService.Default> property from your initialization code (typically `Program.Main`). Call the <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method to get a new instance of the service.

    > [!IMPORTANT]
    > The caching service must be initialized before any cached method is called for the first time.

When dependency injection is disabled, we can also cache `static` methods. Observe the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> in the static `CloudCalculator` implementation.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted_NoDI/GettingStarted_NoDI.cs ]

## What's next

So far, so good. However, if your cached methods have more complex parameters than intrinsic types like `int` or `string` (and a dozen other well-known types), Metalama Caching uses the `ToString` method to represent the parameter in the caching key. This approach may not always be appropriate. In the next article, we'll discuss how to customize the caching key.

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-keys>
> <xref:caching-invalidation>
> <xref:dependency-injection>
