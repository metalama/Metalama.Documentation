---
uid: caching
summary: "Metalama Caching is an efficient way to enhance application performance, reducing boilerplate, bugs, and coupling. It supports various caching topologies and automates complex cache processes. "
keywords: "caching, cache key generation, cache invalidation, performance enhancement, Metalama Caching, .NET, reduced boilerplate, flexible caching topologies"
created-date: 2024-06-11
modified-date: 2025-11-30
---
# Metalama.Patterns.Caching

Caching is an effective method to enhance application performance. Traditionally, implementing caching requires interacting with the API of the caching framework (such as <xref:System.Runtime.Caching.MemoryCache>) or database (like Redis). It also involves incorporating moderately complex logic into your source code to generate the cache key, verify the item's existence in the cache, and add the item to the cache. Additional complexity arises from the necessity to remove items from the cache when the source data is updated. Manual caching implementation is time-consuming and prone to errors, as it's easy to generate inconsistent cache keys between read and update methods.

## Benefits

Metalama Caching offers several advantages over manual caching:

- **Reduced boilerplate**: Metalama Caching lets you cache the return value of a method as a function of its arguments with just a custom attribute, specifically the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect. To invalidate the cache, add the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect to the update methods. To use a custom class as a parameter of a cached method, apply the <xref:Metalama.Patterns.Caching.Aspects.CacheKeyAttribute?text=[CacheKey]> aspect to mark the properties that uniquely identify the object. Your business code becomes shorter and more readable.

- **Reduced bugs**: Manually generating cache keys with hand-written code is notorious for being bug-prone. Metalama Caching eliminates this source of defects by implementing a reliable approach to key generation, combining object-oriented and aspect-oriented techniques.

- **Reduced coupling**: Cache invalidation can be complex and often requires you to review your complete _write_ methods every time you add caching to a _read_ method. Cache dependencies act as an abstraction layer between _read_ and _write_ methods, reducing coupling between them.

- **Flexible topologies**: Metalama Caching supports several caching topologies, letting you switch between them effortlessly:
  - In-memory caching
  - Redis-based distributed caching (see <xref:caching-redis>)
  - Redis-based distributed caching with a synchronized in-memory L1 (see <xref:caching-redis>)
  - In-memory caching with multi-node synchronization over Azure Service Bus or Redis Pub/Sub (see <xref:caching-pubsub>)

## In this chapter

| Section | Description |
|---------|-------------|
| <xref:caching-getting-started> | This article demonstrates how to cache method return values. |
| <xref:caching-invalidation> | This article illustrates how to declaratively and imperatively invalidate cached method return values. |
| <xref:caching-dependencies> | This article explains how to automatically invalidate cache items using cache dependencies. |
| <xref:caching-keys> | This article provides guidance on customizing the cache keys that identify cached method return values. |
| <xref:caching-redis> | This article shows how to use Redis as a distributed cache. |
| <xref:caching-pubsub> | This article demonstrates how to invalidate all related in-memory caches in a distributed environment. |
| <xref:caching-value-adapters> | This article describes how to cache return values of methods that cannot be directly cached, such as instances of <xref:System.Collections.Generic.IEnumerable`1> or <xref:System.IO.Stream>. |
| <xref:cache-locking> | This article explains how to prevent the same method from being executed with the same arguments simultaneously by using locking. |
| <xref:caching-troubleshooting> | This article details how to add logging to the caching component. |

> [!div class="see-also"]
> <xref:patterns>
> <xref:memoization>
