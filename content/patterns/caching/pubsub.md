---
uid: caching-pubsub
summary: "The document provides a guide on synchronizing local in-memory caches across multiple servers using Metalama's publish/subscribe (Pub/Sub) message bus. It details the process using Azure Service Bus and Redis Pub/Sub."
keywords: "distributed caching, cache invalidation, in-memory cache, Azure Service Bus, Redis Pub/Sub, Metalama, publish/subscribe, cache synchronization, cache consistency"
created-date: 2024-04-25
modified-date: 2024-08-04
---
# Synchronizing local in-memory caches for multiple servers

> [!NOTE]
> This feature requires a Metalama Professional license.

Caching in distributed applications can pose a complex problem. When multiple instances of an application are running simultaneously (typically websites or web services deployed in the cloud or web farms), it's crucial to ensure that the cache is appropriately invalidated for all application instances.

A common solution to this issue is the use of a centralized cache server (or a cluster of cache servers), such as a Redis server or a Redis cluster. However, operating a cache server or cluster incurs a cost, which may not always be justified for medium-sized applications, such as a small business website.

An alternative solution to distributed caching is to maintain a local in-memory cache in each application instance. Instead of using a shared distributed cache, each application instance caches its data into its local cache. However, when one application instance modifies a piece of data, it must ensure that all instances remove the relevant items from their local cache. This process is known as *distributed cache invalidation*. It can be achieved easily and inexpensively with a publish/subscribe (Pub/Sub) message bus, which is much less costly than a cache cluster.

Metalama facilitates the easy addition of pub/sub cache invalidation to your existing Metalama caching using either Azure Service Bus or Redis Pub/Sub.

> [!WARNING]
> With pub/sub invalidation, there may be some latency in the invalidation mechanism, i.e., different application instances running on different servers may see different data for a few dozen milliseconds. While generally harmless when application clients are affinitized to one server (for instance, with geo-based request routing), it can cause issues when the same client can randomly connect to different servers.


## Using Azure Service Bus


### Configuring a topic

The first step is to create a topic. To do this using the Microsoft Azure portal, follow these steps:

1. Navigate to the Microsoft Azure portal, open the **Service Bus** panel and create a new **Topic**. Choose a small value for the time-to-live setting, such as 30 seconds. Visit the [Microsoft Azure website](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-topics-subscriptions-portal) for more details.

2. In the Microsoft Azure portal, create a **Shared access policy** and include the **Send**, **Listen**, and **Manage** rights. Your application will use this policy.

3. Copy the primary or secondary connection string to your clipboard.


### Configuring your application

1. Add caching to your application as described in <xref:caching-getting-started>.

2. Add a reference to the [Metalama.Patterns.Caching.Backends.Azure](https://www.nuget.org/packages/Metalama.Patterns.Caching.Backends.Azure/) NuGet package.

3. Return to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching> or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Building.CachingBackendFactory.Memory*> method. Then, call <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCachingFactory.WithAzureSynchronization*> and pass the topic connection string as an argument.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AzureSynchronized/AzureSynchronized.Program.cs marker="AddMetalamaCaching"]

4. We recommend initializing the caching service during your application's initialization sequence; otherwise, the service will be initialized lazily upon its first use. Retrieve the <xref:Metalama.Patterns.Caching.ICachingService> interface from the <xref:System.IServiceProvider> and call the <xref"Metalama.Patterns.Caching.ICachingService.InitializeAsync*> method.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AzureSynchronized/AzureSynchronized.Program.cs marker="Initialize"]


> [!WARNING]
> Ensure that the <xref:Metalama.Patterns.Caching.ICachingService> is properly disposed of before the application exits. Failure to do so may leave some background cache write operations unprocessed, leading to cache inconsistency.


### Example: A Distributed Application Synchronized by Azure Service Bus

The following example simulates a multi-instance application. For ease of testing, both instances live in the same process. Both instances read and write to a shared database simulated by a concurrent dictionary, which sits behind an in-memory cache. These two cache instances are synchronized using <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCachingFactory.WithAzureSynchronization*>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/AzureSynchronized/AzureSynchronized.cs]


## Using Redis Pub/Sub

If you are already using Redis as a storage for Metalama Caching, adding another layer of invalidation is unnecessary as this is already handled by the Redis caching back-end. However, if you already have a Redis cluster but don't want to use it for caching, you can still use it for cache invalidation. An example of this situation is when your Redis server's latency is too high for caching but sufficient for cache invalidation.

No configuration on your Redis server is necessary to use it for cache synchronization.

1. Add caching to your application as described in <xref:caching-getting-started>.
2. Add a reference to the [Metalama.Patterns.Caching.Backends.Redis](https://www.nuget.org/packages/Metalama.Patterns.Caching.Backends.Redis/) NuGet package.

3. Return to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching> or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Building.CachingBackendFactory.Memory*> method. Then, call <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.WithRedisSynchronization*> and pass an instance of <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheSynchronizerConfiguration>.

4. We recommend initializing the caching service during your application's initialization sequence; otherwise, the service will be initialized lazily upon its first use. Retrieve the <xref:Metalama.Patterns.Caching.ICachingService> interface from the <xref:System.IServiceProvider> and call the <xref"Metalama.Patterns.Caching.ICachingService.InitializeAsync*> method.

> [!WARNING]
> Ensure that the <xref:Metalama.Patterns.Caching.ICachingService> is properly disposed of before the application exits. Failure to do so may leave some background cache write operations unprocessed, leading to cache inconsistency.



