---
uid: release-notes-2026.1
level: 200
summary: "Metalama 2026.1 brings major improvements to the Redis caching backend, including a resilience framework, key compression, and overload detection."
keywords: "Metalama 2026.1, release notes, caching, Redis, resilience, retry policy"
created-date: 2026-04-13
modified-date: 2026-04-13
---

# Metalama 2026.1

Metalama 2026.1 brings major improvements to the Redis caching backend, porting significant features from PostSharp.

**Highlights:**

- **Resilience framework** for the Redis caching backend with configurable retry and exception handling policies
- **Key compression** for long cache keys using xxHash algorithms
- **Background task overload detection** to prevent system strain during peak load
- **New configuration options** for concurrency limits, command flags, and garbage collector behavior

## Redis caching backend improvements

The Redis caching backend has been significantly enhanced with features ported from PostSharp.Patterns.Caching. For comprehensive documentation, see <xref:caching-redis>.

### Resilience framework

The new resilience framework replaces the previous `ExceptionHandlingCachingBackendEnhancer` with built-in retry and exception handling policies directly on the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration> class.

- **Retry policies** (<xref:Metalama.Patterns.Caching.Resilience.IRetryPolicy>): Configurable retry logic with exponential backoff and jitter for transactions, background tasks, and recovery actions.
- **Exception handling policies** (<xref:Metalama.Patterns.Caching.Resilience.IExceptionHandlingPolicy>): Control how exceptions are handled after retry attempts are exhausted. The <xref:Metalama.Patterns.Caching.Resilience.DefaultExceptionHandlingPolicy> logs exceptions and attempts to recover from failed write operations.

### Key compression

Cache keys that exceed a configurable threshold (default: 128 characters) can now be automatically hashed using the <xref:Metalama.Patterns.Caching.Formatters.CacheKeyHashingAlgorithm> enum (`XxHash64` or `XxHash128`), avoiding Redis key length limits and improving performance with long keys.

### Overload detection

The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend> now monitors its background task queue and exposes the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend.IsBackgroundTaskQueueOverloaded> property and <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend.IsBackgroundTaskQueueOverloadedChanged> event. The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollector> automatically pauses real-time notification processing when the backend is overloaded.

### New configuration options

Several new properties have been added to <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration>:

- `TransactionRetryPolicy`, `BackgroundTasksRetryPolicy`, `BackgroundRecoveryRetryPolicy` for resilience configuration
- `BackgroundTasksMaxConcurrency` and `BackgroundTasksOverloadedThreshold` for overload management
- `InvalidationMaxConcurrency` for throttling large graph invalidations
- `KeyCompressingThreshold` for key compression
- `DisposeTimeout`, `SupportsEvents`, `ReadCommandFlags`, `WriteCommandFlags`

The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheDependencyGarbageCollectorOptions> class now provides configuration for the periodic cleanup process, including `CacheCleanupDelay` and <xref:Metalama.Patterns.Caching.Implementation.CacheCleanupOptions> (with `RemediationDelay` and `MaxConcurrency`).

## Breaking changes

- **Data schema redesign**: The Redis data schema has been redesigned. A cache purge (`FLUSHDB`) is required after upgrading to 2026.1.
- **Removed `ExceptionHandlingCachingBackendEnhancer`**: Use the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.ExceptionHandlingPolicy> property instead.
- **`TransactionMaxRetries` is obsolete**: Use <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackendConfiguration.TransactionRetryPolicy> instead.
- **`CacheCleanupOptions.Sequential` is obsolete**: Use <xref:Metalama.Patterns.Caching.Implementation.CacheCleanupOptions.MaxConcurrency> instead.
- **`CacheValue.Dependencies` / `CacheItem.Dependencies`**: Now expose first-level dependencies only (not recursive).

> [!div class="see-also"]
> <xref:release-notes>
> <xref:caching-redis>
