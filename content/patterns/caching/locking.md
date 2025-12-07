---
uid: cache-locking
summary: "The document discusses how to prevent concurrent execution of cached methods in Metalama using a lock manager. It covers local and distributed lock strategies, handling lock timeouts, and implementing a distributed lock manager."
keywords: "concurrent execution, cached methods, lock manager, local lock strategy, distributed lock manager, Metalama, locking strategy, ILockingStrategy interface, CachingProfile, lock timeouts"
created-date: 2024-04-25
modified-date: 2025-11-30
---
# Preventing concurrent execution of cached methods

When a method's evaluation consumes substantial resources or time, it may be advisable to prevent multiple threads, processes, or machines from evaluating the same method with identical parameters concurrently. This can be achieved by instructing Metalama to employ a _lock manager_, abstracted by the <xref:Metalama.Patterns.Caching.Locking.ILockingStrategy> interface.

Metalama provides two lock strategies: the default <xref:Metalama.Patterns.Caching.Locking.NullLockingStrategy>, and <xref:Metalama.Patterns.Caching.Locking.LocalLockingStrategy>.

## Preventing concurrent execution in the current process

By default, the caching aspect permits concurrent execution of the same method with identical arguments.

The <xref:Metalama.Patterns.Caching.Locking.LocalLockingStrategy> class implements a locking strategy that prevents methods running in the current process (or more precisely, the <xref:System.AppDomain>) from executing concurrently.

To configure the lock manager, the <xref:Metalama.Patterns.Caching.CachingProfile.LockingStrategy> property of the relevant <xref:Metalama.Patterns.Caching.CachingProfile> must be set. Each caching profile needs to be configured separately.

To start using <xref:Metalama.Patterns.Caching.Locking.LocalLockingStrategy>, navigate to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching> or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Supply a delegate that calls <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.AddProfile*> and sets the <xref:Metalama.Patterns.Caching.CachingProfile.LockingStrategy?CachingProfile.LockingStrategy> property.

> [!NOTE]
> Each instance of the <xref:Metalama.Patterns.Caching.Locking.LocalLockingStrategy> class maintains its own set of locks. However, it is irrelevant whether several profiles use the same or a different instance of the <xref:Metalama.Patterns.Caching.Locking.LocalLockingStrategy>, as each method is associated with one and only one profile.

For instance, the following snippet activates <xref:Metalama.Patterns.Caching.Locking.LocalLockingStrategy> for the `Locking` caching profile:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/Locking/Locking.Program.cs marker="AddMetalamaCaching"]

### Example: locking vs non-locking caching access

The following example demonstrates two versions of a simulated `ReadFile` method: one without cache locking, and the second with cache locking. The fake implementations ensure deterministic behavior.

The main program executes these methods twice in parallel and compares their results. When locking is enabled, both executions return exactly the same instance, indicating that the methods did _not_ execute in parallel. This is precisely the purpose of cache locking.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Locking/Locking.cs]

## Handling lock timeouts

By default (unless the default <xref:Metalama.Patterns.Caching.Locking.NullLockingStrategy> is used), the caching aspect waits indefinitely for a lock. Suppose the thread evaluating the method becomes stuck (e.g., it's involved in a deadlock). Due to the locking mechanism, all threads evaluating the same method will also become stuck. To avoid this situation, implement a timeout behavior.

> [!NOTE]
> This section only covers the time taken to acquire a lock. It does not address the execution time of the method that has already acquired the lock.

Two properties of the <xref:Metalama.Patterns.Caching.CachingProfile> class influence the timeout behavior:

* <xref:Metalama.Patterns.Caching.CachingProfile.AcquireLockTimeout> determines the maximum time that the caching aspect waits for the lock manager to acquire a lock. To specify an infinite waiting time, set this property to `TimeSpan.FromMilliseconds(-1)`. The default behavior is to wait indefinitely.

* <xref:Metalama.Patterns.Caching.CachingProfile.OnLockTimeout> is a delegate invoked when the caching aspect can't acquire a lock due to a timeout. The default behavior is to throw a <xref:System.TimeoutException>. To ignore the lock and proceed with the method implementation, replace this property with a delegate that does nothing.

## Implementing a distributed lock manager

Implementing a distributed locking algorithm is a complex task, and we at Metalama have chosen not to go deeper into this area (just as we don't provide the implementation of a cache itself). However, Metalama does offer the ability to use any third-party implementation.

To make your lock manager work with the caching aspect, implement the <xref:Metalama.Patterns.Caching.Locking.ILockingStrategy> and <xref:Metalama.Patterns.Caching.Locking.ILockHandle> interfaces.

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-configuration>
> <xref:caching-redis>
