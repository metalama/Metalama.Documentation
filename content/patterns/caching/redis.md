---
uid: caching-redis
summary: "The document provides a guide on how to use Redis as a distributed server for caching in a Metalama application, including configuration, resilience policies, key compression, and handling dependencies."
keywords: "Redis caching, distributed caching, in-memory L1 cache, Metalama, StackExchange.Redis, Azure Redis Cache, Redis Pub/Sub, caching backend configuration, retry policy, exception handling, key hashing, resilience"
created-date: 2024-04-25
modified-date: 2026-04-13
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

### Example: caching using Redis

Here's an update of the example used in <xref:caching-getting-started>, modified to use Redis instead of `MemoryCache` as the caching back-end.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Redis/Redis.cs]

### Without dependency injection

If you aren't using dependency injection:

1. Create a [StackExchange.Redis.ConnectionMultiplexer](https://stackexchange.github.io/StackExchange.Redis/Configuration).

2. Call <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>, then the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method, and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.Redis*> method. Pass a <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration> and set the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.Connection> property to your `ConnectionMultiplexer`.

## Resilience and performance

The Redis caching backend includes a built-in resilience framework that handles transient failures through retry policies and exception handling policies. This replaces the previous `ExceptionHandlingCachingBackendEnhancer` approach used in earlier versions.

### Retry policies

Retry policies control how failed Redis operations are retried. The <xref:Metalama.Patterns.Caching.Resilience.IRetryPolicy> interface defines the contract, and the default implementation <xref:Metalama.Patterns.Caching.Resilience.RetryPolicy> uses exponential backoff with jitter.

The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration> exposes three retry policy properties:

| Property | Default | Description |
|----------|---------|-------------|
| <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.TransactionRetryPolicy> | `TransactionRetryPolicy` | Handles retries for Redis transactions that fail due to data conflicts. |
| <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundTasksRetryPolicy> | `BackgroundRetryPolicy` | Handles retries for non-blocking background operations. |
| <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundRecoveryRetryPolicy> | `BackgroundRetryPolicy` | Handles retries for recovery actions such as <xref:Metalama.Patterns.Caching.Resilience.RecoveryAction.InvalidateDependencyInBackground> or <xref:Metalama.Patterns.Caching.Resilience.RecoveryAction.RemoveItemInBackground>. |

The <xref:Metalama.Patterns.Caching.Resilience.RetryPolicy> class exposes the following configurable properties:

| Property | Type | Default |
|----------|------|---------|
| `BaseDelay` | `TimeSpan` | 25 ms |
| `Multiplier` | `double` | 1.2 |
| `MaxDelay` | `TimeSpan` | 2 s |
| `JitterFactor` | `double` | 0.2 |
| `MaxAttempts` | `int` | 5 |
| `NoDelayAttempts` | `int` | 1 |

### Exception handling policies

The <xref:Metalama.Patterns.Caching.Resilience.IExceptionHandlingPolicy> interface allows you to control how exceptions are handled after all retry attempts have been exhausted. The <xref:Metalama.Patterns.Caching.Resilience.DefaultExceptionHandlingPolicy> logs exceptions and attempts to recover from failed write operations.

Set the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.ExceptionHandlingPolicy> property to customize this behavior.

The exception handling policy receives an <xref:Metalama.Patterns.Caching.Resilience.ExceptionInfo> object describing the exception and returns a <xref:Metalama.Patterns.Caching.Resilience.RecoveryAction> indicating how to proceed:

| Recovery action | Description |
|-----------------|-------------|
| `Swallow` | The exception is silently consumed. |
| `Rethrow` | The exception is re-thrown to the caller. |
| `RemoveItemInBackground` | The cache item that caused the exception is removed asynchronously. |
| `InvalidateDependencyInBackground` | The dependency that caused the exception is invalidated asynchronously. |

The <xref:Metalama.Patterns.Caching.Resilience.OperationKind> enum identifies which operation triggered the exception, allowing the policy to make context-specific decisions.

### Key compression

When cache keys exceed a certain length, they can cause performance issues or hit Redis key length limits. The Redis caching backend can automatically hash long keys using the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.KeyCompressingThreshold> property.

When a cache key exceeds the threshold (default: 128 characters), it is hashed using the algorithm specified by the <xref:Metalama.Patterns.Caching.Formatters.CacheKeyHashingAlgorithm> enum:

| Algorithm | Description |
|-----------|-------------|
| `None` | No hashing (default). |
| `XxHash64` | 64-bit xxHash — fast, low collision rate. |
| `XxHash128` | 128-bit xxHash — negligible collision rate. |

### Concurrency and overload detection

The Redis caching backend manages several types of concurrent operations and provides mechanisms to prevent system overload.

#### Background task management

Many Redis operations (write-through for L1 caches, invalidation propagation, recovery actions) are executed in the background. The following configuration properties control concurrency:

| Property | Default | Description |
|----------|---------|-------------|
| <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundTasksMaxConcurrency> | 25 | Maximum number of concurrent background tasks. |
| <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundTasksOverloadedThreshold> | 125 | Number of queued tasks above which the backend reports an overloaded state. |
| <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.InvalidationMaxConcurrency> | 20 | Maximum number of concurrent invalidation operations per call. |

#### Overload detection

The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend> exposes an overload detection mechanism through the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend.IsBackgroundTaskQueueOverloaded> property and the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend.IsBackgroundTaskQueueOverloadedChanged> event. When the number of queued background tasks exceeds the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.BackgroundTasksOverloadedThreshold>, the backend notifies dependent components. In particular, the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> temporarily stops processing real-time eviction and expiration notifications during overload to prevent further strain on the system.

### Example: configuring resilience and performance

The following example shows how to customize retry policies, concurrency limits, and key compression when initializing the Redis caching backend:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Redis/RedisConfiguration.Program.cs marker="ConfigureResilience"]

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
* From a deployment perspective, the server requires a garbage collection service to run continuously, even when the app isn't running. This service cleans up dependencies when cache items are expired from the cache.

If you choose to enable dependencies with Redis, ensure that at least one instance of the cache GC process is running. It's legal to have several instances of this process running, but since all instances compete to process the same messages, it's better to ensure that only a small number of instances (ideally one) is running.

To enable dependencies, set the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.SupportsDependencies?text=RedisCachingBackendConfiguration.SupportsDependencies> property to `true` when initializing the Redis caching back-end.

> [!WARNING]
> Caching dependencies can't be used on a [Redis cluster](https://redis.io/docs/latest/operate/oss_and_stack/management/scaling/). Only the [master-replica](https://redis.io/docs/latest/operate/oss_and_stack/management/replication/) topology is supported with caching dependencies. This limitation exists because a cache operation with dependencies is implemented as a transaction of several operations, which must all reside on the same node.

### Running the dependency GC process

The recommended approach to run the dependency GC process is to create an application host using the `Microsoft.Extensions.Hosting` namespace. The GC process implements the `IHostedService` interface. To add it to the application, use the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.AddRedisCacheDependencyGarbageCollector*> extension method.

In case of an outage of the service running the GC process, execute the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisGarbageCollectionUtilities.PerformFullCollectionAsync*> method.

The following program demonstrates this:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/RedisGC/RedisGC.cs]

### Configuring the dependency GC

The garbage collector can be configured using the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollectorOptions> class, which exposes the following properties:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `CacheCleanupDelay` | `TimeSpan` | 4 hours | Delay between subsequent periodic cleanups. |
| `CacheCleanupOptions` | <xref:Metalama.Patterns.Caching.Implementation.CacheCleanupOptions> | `WaitDelay`=100 ms, `MaxConcurrency`=1 | Options for the periodic cleanup operation. |

The <xref:Metalama.Patterns.Caching.Implementation.CacheCleanupOptions> class controls the cleanup behavior:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `WaitDelay` | `TimeSpan` | 0 | Delay between processing two keys. |
| `RemediationDelay` | `TimeSpan` | 10 s | Delay before re-checking an inconsistency for remediation. This accounts for replication lag in distributed setups. |
| `MaxConcurrency` | `int` | 20 | Maximum number of keys analyzed concurrently. |
| `Dry` | `bool` | `false` | When `true`, reports errors without attempting to fix them. |

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-getting-started>
> <xref:caching-pubsub>
> <xref:caching-dependencies>
