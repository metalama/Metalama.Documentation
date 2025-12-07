---
uid: memoization
summary: "Memoization is an optimization technique that caches results of deterministic methods to enhance performance. Metalama provides a straightforward and high-performance implementation through the [Memoize] aspect."
level: 200
keywords: "memoization, optimization, caching, Metalama, Memoize aspect, high-performance caching, System.Lazy alternative, .NET"
created-date: 2024-05-13
modified-date: 2025-11-30
---

# Metalama.Patterns.Memoization

Memoization is an optimization technique that enhances the performance of deterministic methods by caching their results. Metalama provides a straightforward and high-performance implementation through the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect.

This aspect is limited to get-only properties and parameterless methods. The cached value of memoized methods and properties is stored in a field of the object itself, enabling a high-performance implementation using `Interlocked.CompareExchange`. It serves as an alternative to the <xref:System.Lazy`1> class, offering simpler usage and superior performance characteristics.

To memoize a property or a method:

1. Add the [Metalama.Patterns.Memoization](https://www.nuget.org/packages/Metalama.Patterns.Memoization/) package to your project.
2. Apply the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> attribute to the get-only property or parameterless method.

> [!WARNING]
> The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect doesn't guarantee that the method will be executed only once. However, it ensures that it always returns the same value or object.

> [!NOTE]
> For nullable reference types and value types, the cached value is stored in a <xref:System.Runtime.CompilerServices.StrongBox`1>, adding some memory allocation overhead when many memoized properties or methods are evaluated. However, this allows for minimal memory allocation when few or none of them are evaluated.

## Example: Memoization

The following example demonstrates typical use of the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect. It presents a `HashedBuffer` class where the `Hash` property and the `ToString` method are optimized for performance. The example assumes that these members are evaluated for only a minority of `HashedBuffer` instances, so the hash shouldn't be pre-computed in the constructor. However, when they're evaluated, the example assumes they're evaluated often, so caching the result is beneficial. The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect offers a solution that's both simpler and more performant than the <xref:System.Lazy`1> class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Memoization/Memoize.cs]

## Memoization vs. caching

Memoization can be considered a simple form of caching. The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect is often a no-brainer—it's extremely simple to use and requires no infrastructure.

| Factor                        | Memoization                                                    | Caching                                                                                                       |
|-------------------------------|----------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------|
| **Scope**                     | Local to a single class instance within the current process    | Either local or shared, when run as an external service such as Redis                                        |
| **Unicity of cache items**    | Specific to the current instance or type                       | Based on explicit `string` cache keys                                                                         |
| **Complexity and overhead**   | Minimal overhead                                               | Significant overhead related to the generation of cache keys and, in the case of distributed caching, serialization |
| **Expiration and invalidation** | No expiration or invalidation                                | Advanced and configurable expiration policies and invalidation APIs                                           |

> [!div class="see-also"]
> <xref:patterns>
> <xref:caching>
