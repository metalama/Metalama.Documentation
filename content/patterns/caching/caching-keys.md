---
uid: caching-keys
summary: "The document provides comprehensive guidelines on customizing cache keys in Metalama Caching, including using the CacheKey aspect, overriding ToString method, implementing IFormattable<T> interface, creating a formatter for third-party types, changing the maximal length of a cache key, and overriding the cache key builder."
keywords: "customize cache keys, Metalama Caching, CacheKey aspect, overriding ToString, IFormattable<T> interface, cache key builder, caching methods, CacheKeyAttribute, formatter for third-party types"
created-date: 2024-04-25
modified-date: 2025-11-30
---
# Customizing cache keys

By default, the cache key of a parameter is built using the `ToString` method. However, the default implementation of the `ToString` method doesn't return a unique string for custom classes and structs. The default implementation of `ToString` for records is more likely to be correct. Therefore, provide a cache key implementation for all parameter types of a cached method. This article explains several approaches.

## Using the [CacheKey] aspect

The most straightforward approach to customize the cache key for a `class` or `struct` is to add the `[CacheKey]` aspect to the fields or properties that must be a part of the cache key.

This aspect automatically implements the <xref:Flashtrace.Formatters.IFormattable`1> interface for the <xref:Metalama.Patterns.Caching.Formatters.CacheKeyFormatting> role.

### Example: [CacheKey] aspect

The following example demonstrates a service class `EntityService` and an entity class `Entity`. The method `EntityService.GetRelatedEntities` retrieves all entities related to a given `Entity` and is cached using the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect. Therefore, the `Entity` class is a part of the cache key. Any `Entity` is uniquely distinguished by its `Id` and `Kind` properties. We use the <xref:Metalama.Patterns.Caching.Aspects.CacheKeyAttribute?text=[CacheKey]> aspect on these properties to add these properties to the cache key. However, the `Description` property isn't a part of the entity identity and doesn't require the aspect.

You can observe how the <xref:Metalama.Patterns.Caching.Aspects.CacheKeyAttribute?text=[CacheKey]> aspect implements the <xref:Flashtrace.Formatters.IFormattable`1> interface.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/CacheKeyAspect.cs]

## Overriding the ToString method or ISpanFormattable interface

For simple types, consider implementing the <xref:System.Object.ToString*> method to return a distinct value for each distinct instance of the type.

Since <xref:System.Object.ToString*> always allocates a short-lived `string`, which presents a minor performance overhead, an alternative is to implement the <xref:System.ISpanFormattable> interface. However, the optimization level of Metalama Caching is not so high that using <xref:System.ISpanFormattable> instead of <xref:System.Object.ToString*> would make a significant difference at the moment.

The inconvenience of either of these approaches is that <xref:System.Object.ToString*> and <xref:System.ISpanFormattable> are typically used to create human-readable strings, which may conflict with the goal of creating cache keys. Whenever these goals conflict, take a different approach.

This approach is mentioned because this is the fallback mechanism: if Metalama Caching finds no other way to generate a cache key from an object, it will first see if <xref:System.ISpanFormattable> is implemented, and, if not, it will use <xref:System.Object.ToString*>.

## Implementing the IFormattable<T> interface

If none of the above approaches are suitable, you can manually implement the <xref:Flashtrace.Formatters.IFormattable`1> interface, where `T` is the <xref:Metalama.Patterns.Caching.Formatters.CacheKeyFormatting> class.

For inspiration, see the aspect-generated code of the `[CacheKey]` example above.

> [!WARNING]
> It's a best practice to include the full type name in all generated strings. Suppose for instance you have a class family representing database entities. The cache key of each entity is the `Id` property. If you don't include the type name in the cache key, you won't be able to differentiate a `Customer` from an `Invoice` that have the same `Id`, which may cause a problem in situations where the objects are passed as parameters of the same method.

## Implementing a formatter for a third-party type

If you don't own the source code of a type, none of the approaches mentioned above can work. In this situation, follow these steps:

### Step 1. Implement the Formatter<T> class

Create a class derived from the <xref:Flashtrace.Formatters.Formatter`1> abstract class where `T` is the type for which you want to generate cache keys.

> [!NOTE]
> Your formatter class can have generic parameters; in this case, they have to match the generic parameters of the formatted type. One of these generic parameters can represent the formatted type itself. This parameter must have the <xref:Flashtrace.Formatters.TypeExtensions.BindToExtendedTypeAttribute?text=[BindToExtendedType]> custom attribute.

Then, implement the <xref:Flashtrace.Formatters.Formatter`1.Format*> abstract method.

### Step 2. Register your new formatter

Return to the code that initialized Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>, and supply a delegate that calls <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.ConfigureFormatters*> like in the following snippet:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Formatter/Formatter.Program.cs marker="Registration"]

### Example: custom formatter for FileInfo

In this example, we demonstrate how to build a custom cache key formatter for the `System.IO.FileInfo` class, whose `ToString` implementation returns the file name instead of the full path and is therefore unsuitable for use in a cache key. The formatter is implemented by the `FileInfoFormatter` class, which is registered during app initialization. With this, the `FileSystem` service can safely use `System.IO.FileInfo` in cached methods.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Formatter/Formatter.cs]

## Changing the maximal length of a cache key

The maximum length of a cache key is 1024 characters by default.

To change the maximum length of a cache key, the procedure is similar to registering a custom formatter.

Go to the code that initialized Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. This time, call <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithKeyBuilderOptions*> and pass a new instance of the `CacheKeyBuilderOptions` with the `MaxKeySize` property set to a different value.

> [!WARNING]
> If you need large cache keys, we suggest you also hash the cache key before submitting it to the caching backend. To hash the cache key, implement a custom cache key builder. We will show how to achieve this in the next section.

## Overriding the cache key builder

The ultimate and least necessary solution to customize the cache key is to provide your own implementation of the <xref:Metalama.Patterns.Caching.Formatters.ICacheKeyBuilder> interface.

The default implementation is the <xref:Metalama.Patterns.Caching.Formatters.CacheKeyBuilder> class. It has many `virtual` methods that you can override. It generates the cache key by appending the following items:

* In case the backend supports it, a global prefix that allows using the same caching server with several applications (see e.g. <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.KeyPrefix>)

* The full name of the declaring type (including generic parameters, if any)

* The method name

* The method generic parameters, if any

* The `this` object (unless the method is static)

* A comma-separated list of all method arguments including the full type of the parameter and the formatted parameter value

To override the default <xref:Metalama.Patterns.Caching.Formatters.ICacheKeyBuilder> implementation:

1. Create a new class that implements the <xref:Metalama.Patterns.Caching.Formatters.ICacheKeyBuilder> interface, or derive from <xref:Metalama.Patterns.Caching.Formatters.CacheKeyBuilder> if you want to reuse its logic.

2. Register your implementation while calling  <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> as shown in the following snippet:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/HashingKeyBuilder/HashingKeyBuilder.Program.cs]

### Example: implementing a hashing cache key builder

In this example, we show how to build and register a custom key builder. We chose the `XxHash128` algorithm because it has good performance and very low collision.

Note that we're reusing the string-based <xref:Metalama.Patterns.Caching.Formatters.CacheKeyBuilder> implementation so we can reuse the infrastructure described in this article. It's theoretically possible to implement a hashing string builder that doesn't rely on any string, but it would require us to design and implement a new solution, one that wouldn't rely on the string-based <xref:Flashtrace.Formatters.IFormattable`1>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/HashingKeyBuilder/HashingKeyBuilder.cs]

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-getting-started>
> <xref:caching-exclude-parameters>
