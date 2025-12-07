---
uid: caching-invalidation
summary: "The document explains how to use Metalama for cache invalidation in software engineering, detailing both declarative and imperative methods, and how to force cache to refresh. It also discusses compile-time validation."
keywords: "cache invalidation, Metalama, remove from cache, caching keys, InvalidateCache attribute, CachingServiceExtensions"
created-date: 2024-08-04
modified-date: 2025-11-30
---
# Invalidating the cache

As Phil Karlton once [famously said](https://www.karlton.org/2017/12/naming-things-hard/), _"There are only two hard things in Computer Science: cache invalidation and naming things."_

This quote humorously captures the deceptive complexity of cache invalidation. It might seem straightforward, but it's notoriously difficult to get right.

Metalama can't completely eliminate this problem from the landscape of software engineering challenges. Cache invalidation is and will remain challenging, especially in the context of distributed systems where multiple caches and data stores need to be kept in sync. However, Metalama simplifies the task of requesting the removal of the correct cache item by exposing a rich programmatic and aspect-oriented API, and eliminating the need to generate consistent caching keys between the cached method and the invalidating logic.

Metalama offers two ways to invalidate the cache:

- _Direct_ invalidation is when the method that _updates_ the application state (such as a database entity) directly invalidates the cache for all _read_ methods that depend on this entity. The benefit of direct invalidation is that it doesn't require a lot of resources on the caching backend. However, this approach has a significant disadvantage: it exhibits an imperfect separation of concerns. Update methods need to have detailed knowledge of all cached read methods, therefore update methods need to be modified whenever a read method is added. This article will only cover direct invalidation.

- _Indirect_ invalidation adds a layer of abstraction, named _cache dependencies_, between the cached method and the invalidating code. Read methods are responsible for adding the proper dependencies to their context, and update methods are responsible for invalidating the dependencies. Therefore, update methods no longer need to know all read methods. For details about this approach, see <xref:caching-dependencies>.

## Invalidating cache items declaratively using the [InvalidateCache] aspect

You can add the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect to a method (referred to as the _invalidating method_) to cause any call to this method to remove from the cache the value of one or more other methods. Parameters of both methods are matched by name and type.

By default, the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect looks for the cached method in the current type. You can specify a different type using the alternative constructor of the custom attribute.

For instance, suppose you've the following read method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateAspect/InvalidateAspect.cs marker="Cache"]

The following code would invalidate this method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateAspect/InvalidateAspect.cs marker="InvalidateCache"]

### Example: Using [InvalidateCache]

The following example demonstrates a service named `PriceCatalogue` with two read methods, `GetProducts` and `GetPrice`, and two write methods, `AddProduct` and `UpdatePrice`. The write methods use the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect to trigger invalidation of their respective read methods.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateAspect/InvalidateAspect.cs]

## Compile-time validation

One of the most useful features of the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect is that it verifies that the parameters of the invalidated and invalidating methods match:

- When the method can't be found, a compile-time error is reported.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateCompileTimeErrors/BadMethod.cs]

- If any parameter of the cached method can't be matched with a parameter of the invalidating method, a build error is reported (unless the parameter has the <xref:Metalama.Patterns.Caching.NotCacheKeyAttribute> custom attribute). The order of parameters isn't considered.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateCompileTimeErrors/BadParameter.cs]

- When you invalidate a non-static method (unless instance has been excluded from the cache key by setting the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.IgnoreThisParameter> to `true`), you can do it only from a non-static method of the current type.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateCompileTimeErrors/NonStatic.cs]

- If there are more invalidated methods of the same name for one invalidating method, a build error is reported. To enable invalidation of all the matching overloads by the one invalidating methods, set the property <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute.AllowMultipleOverloads> to `true`.

## Invalidating cache items imperatively

Instead of annotating invalidating methods with a custom attribute, make a call to one of the overloads of the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*> or <xref:Metalama.Patterns.Caching.CachingServiceExtensions.InvalidateAsync*> extension method of the <xref:Metalama.Patterns.Caching.ICachingService> interface.

To access this interface, if you're using dependency injection, first make your class `partial`. Then, the service is available as a field named `_cachingService`. If you aren't using dependency injection in this class, use the <xref:Metalama.Patterns.Caching.CachingService.Default?text=CachingService.Default> property.
The first argument of the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*> method should be a delegate to the method to invalidate. This argument must be followed by the list of arguments for which the cache should be invalidated. These arguments are used to construct the key of the item to be removed from the cache. All arguments must be supplied. Even arguments of parameters that aren't part of the cache key are included.

For instance, suppose you have the following read method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/ImperativeInvalidate/ImperativeInvalidate.cs marker="Cache"]

Then, the following code would invalidate this method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/ImperativeInvalidate/ImperativeInvalidate.cs marker="InvalidateCache"]

### Example: Imperative invalidation

The following example is an update of the previous one. The `PriceCatalogue` service has two read methods, `GetProducts` and `GetPrice`, and two write methods, `AddProduct` and `UpdatePrice`. The write methods use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*> method to trigger invalidation of their respective read methods.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ImperativeInvalidate/ImperativeInvalidate.cs]

## Forcing the cache to refresh

If you want to call a method while skipping the cache, instead of calling <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*?text=_cachingService.Invalidate> and then the method, call the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Refresh*?text=_cachingService.Refresh> method. The new result of the method is stored in the cache.

Contrary to <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*>, <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Refresh*> causes methods in the calling context to skip the cache.

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-getting-started>
> <xref:caching-dependencies>
