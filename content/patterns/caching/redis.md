---
uid: caching-redis
summary: "The document provides a guide on how to use Redis as a distributed server for caching in a Metalama application, including configuration, enabling local in-memory cache, and handling dependencies."
keywords: "Redis caching, distributed caching, in-memory L1 cache, Metalama, StackExchange.Redis, Azure Redis Cache, Redis Pub/Sub, caching backend configuration"
created-date: 2024-04-25
modified-date: 2024-08-04
---

# Using Redis as a distributed cache

> [!NOTE]
> This feature requires a Metalama Professional license.

If you have a distributed application where several instances run in parallel, [Redis](https://redis.io/) is an excellent choice for implementing caching due to the following reasons:

1. **In-Memory Storage**: Redis stores its dataset in memory, allowing for very fast read and write operations, which are significantly faster than disk-based databases.
2. **Rich Data Structures and Atomic Operations**: Redis is not just a simple key-value store; it supports multiple data structures like strings, hashes, lists, sets, sorted sets, and more. Combined with Redis's support for atomic operations on these complex data types, Metalama Caching can implement support for cache dependencies (see <xref:caching-dependencies>).
3. **Scalability and Replication**: Redis provides features for horizontal partitioning or sharding. As your dataset grows, you can distribute it across multiple Redis instances. Redis supports multi-instance replication, allowing for data redundancy and higher data availability. If the master fails, a replica can be promoted to master, ensuring that the system remains available.
4. **Pub/Sub**: Thanks to the Redis Pub/Sub feature, Metalama can synchronize the distributed Redis cache with a local in-memory L1 cache. Metalama can also use this feature to synchronize several local in-memory caches without using Redis storage.

Our implementation uses the [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/) library internally and is compatible with on-premises instances of Redis Cache as well as with the [Azure Redis Cache](https://azure.microsoft.com/en-us/services/cache/) cloud service.

When used with Redis, Metalama Caching supports the following features:

* Distributed caching,
* Non-blocking cache write operations,
* In-memory L1 cache in front of the distributed L2 cache, and
* Synchronization of several in-memory caches using Redis Pub/Sub.

This article covers all these topics.

## Configuring the Redis server

The first step is to prepare your Redis server for use with Metalama caching. Follow these steps:

1. Set up the eviction policy to `volatile-lru` or `volatile-random`. See [https://redis.io/topics/lru-cache#eviction-policies](https://redis.io/topics/lru-cache#eviction-policies) for details.

    > [!CAUTION]
    > Other eviction policies than `volatile-lru` or `volatile-random` are not supported.

2. Set up the key-space notification to include the `AKE` events. See [https://redis.io/topics/notifications#configuration](https://redis.io/topics/notifications#configuration) for details.



## Configuring the caching backend in Metalama

The second step is to configure Metalama Caching to use Redis. 

### With dependency injection

Follow these steps:

1. Add a reference to the [Metalama.Patterns.Caching.Backends.Redis](https://www.nuget.org/packages/Metalama.Patterns.Caching.Backends.Redis/) package.

2. Create a [StackExchange.Redis.ConnectionMultiplexer](https://stackexchange.github.io/StackExchange.Redis/Configuration) and add it to the service collection as a singleton of the `IConnectionMultiplexer` interface type.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Redis/Redis.Program.cs marker="AddRedis"]

    > [!NOTE]
    > If you are using .NET Aspire, simply call `UseRedis()`.    

3. Go back to the code that initialized Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method, and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.Redis*> method.

    Here is an example of the <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*> code.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Redis/Redis.Program.cs marker="AddMetalamaCaching"]


4. We recommend initializing the caching service during the initialization sequence of your application, otherwise the service will be initialized lazily upon first use. Get the <xref:Metalama.Patterns.Caching.ICachingService>   interface from the <xref:System.IServiceProvider> and call the <xref:Metalama.Patterns.Caching.ICachingService.InitializeAsync*> method.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Redis/Redis.Program.cs marker="Initialize"]

### Example: Caching Using Redis

Here is an update of the example used in <xref:caching-getting-started>, modified to use Redis instead of `MemoryCache` as the caching back-end.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Redis/Redis.cs]

### Without dependency injection

If you are not using dependency injection:

1. Create a [StackExchange.Redis.ConnectionMultiplexer](https://stackexchange.github.io/StackExchange.Redis/Configuration).

2. Call <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>, then <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method, supply a delegate that calls the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.Redis*> method. Pass a <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration> and set the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.Connection> property to your `ConnectionMultiplexer`.


## Adding a local in-memory cache in front of your Redis cache

For higher performance, you can add an additional, in-process layer of caching (called L1) between your application and the remote Redis server (called L2).

The benefit of using an in-memory L1 cache is to decrease latency between the application and the Redis server, and to decrease CPU load due to the deserialization of objects. To further decrease latency, write operations to the L2 cache are performed in the background.

To enable the local cache, inside <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>, call the <xref:Metalama.Patterns.Caching.Building.CachingBackendFactory.WithL1*> method right after the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.Redis*> method.

The following snippet shows the updated <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*> code, with just a tiny change calling the <xref:Metalama.Patterns.Caching.Building.CachingBackendFactory.WithL1*> method.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/RedisWithLocalCache/RedisWithLocalCache.Program.cs marker="AddMetalamaCaching"]

When you run several nodes of your applications with the same Redis server and the same <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.KeyPrefix>, the L1 caches of each application node are synchronized using Redis notifications.

> [!WARNING]
> Due to the asynchronous nature of notification-based invalidation, there may be a few milliseconds during which different application nodes may see different values of cache items. However, the application instance initiating the change will have a consistent view of the cache. Short lapses of inconsistencies are generally harmless if the application clients are affinitized to one application node because each application instance has a consistent view. However, if application clients are not affinitized, they may experience cache consistency issues, and the developers who maintain it may lose a few hairs in the troubleshooting process.

## Using dependencies with the Redis caching backend

Metalama Caching's Redis back-end supports dependencies (see <xref:caching-dependencies>), but this feature is disabled by default with the Redis caching backend due to its significant performance and deployment impact:

* From a performance perspective, the cache dependencies need to be stored in Redis (therefore consuming memory) and handled in a transactional way (therefore consuming processing power).
* From a deployment perspective, the server requires a garbage collection service to run continuously, even when the app is not running. This service cleans up dependencies when cache items are expired from the cache.

If you choose to enable dependencies with Redis, you need to ensure that at least one instance of the cache GC process is running. It is legal to have several instances of this process running, but since all instances will compete to process the same messages, it is better to ensure that only a small number of instances (ideally one) is running.

To enable dependencies, set the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.SupportsDependencies?text=RedisCachingBackendConfiguration.SupportsDependencies> property to `true` when initializing the Redis caching back-end.

> [!WARNING]
> Caching dependencies cannot be used on a [Redis cluster](https://redis.io/docs/latest/operate/oss_and_stack/management/scaling/). Only the [master-replica](https://redis.io/docs/latest/operate/oss_and_stack/management/replication/) topology is supported with caching dependencies. The cause of this limitation is that a cache operation with depedencies is implemented as a transaction of several operations, which must all reside on the same node.

### Running the dependency GC process

The recommended approach to run the dependency GC process is to create an application host using the `Microsoft.Extensions.Hosting` namespace. The GC process implements the `IHostedService` interface. To add it to the application, use the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.AddRedisCacheDependencyGarbageCollector*> extension method.

In case of an outage of the service running the GC process, execute the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisGarbageCollectionUtilities.PerformFullCollectionAsync*> method.

The following program demonstrates this:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/RedisGC/RedisGC.cs]



