---
uid: memoization
summary: "Memoization is an optimization technique that caches results of deterministic methods to enhance performance. The technique is implemented through the Memoize aspect in Metalama, offering a simple and high-performance solution compared to the System.Lazy class."
level: 100
keywords: "memoization, optimization, caching, Metalama, Memoize aspect, high-performance caching, System.Lazy alternative, .NET"
created-date: 2024-05-13
modified-date: 2025-11-30
---

# Metalama.Patterns.Memoization

Memoization is an optimization technique that enhances the performance of deterministic methods by caching their results. Metalama provides a straightforward and high-performance implementation of this technique through the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect.

Currently, this aspect is limited to get-only properties and parameterless methods. The cached value of memoized methods and properties is stored in a field of the object itself, enabling a high-performance implementation using `Interlocked.CompareExchange`. It serves as an alternative to the <xref:System.Lazy`1> class, offering a simpler usage and superior performance characteristics.

To memoize a property or a method:

1. Add the [Metalama.Patterns.Memoization](https://www.nuget.org/packages/Metalama.Patterns.Memoization/) package into your project.
2. Apply the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> attribute to the get-only property or parameterless method.


> [!WARNING]
> The current implementation of the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect doesn't guarantee that the method will be executed only once. However, it does ensure that it always returns the same value or object.

> [!NOTE]
> For nullable reference types and for value types, the cached value is stored in a <xref:System.Runtime.CompilerServices.StrongBox`1>, adding some memory allocation overhead in cases where many memoized properties or methods are evaluated. Nevertheless, this allows for minimal memory allocation when few or none of them are evaluated.

## Example: Memoization

The following example demonstrates a typical use of the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect. It presents a `HashedBuffer` class, for which we aim to optimize the performance of the `Hash` property and the `ToString` method. We assume that these members are only evaluated for a minority of instances of the `HashedBuffer` class, therefore the hash should not be pre-computed in the constructor. However, when they are evaluated, we assume they are evaluated often, which means that we should cache the result. The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect offers a solution that is both simpler and more performant than the <xref:System.Lazy`1> class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Memoization/Memoize.cs]


## Memoization vs Caching

Memoization can be considered as a simple form of caching. The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect is often a no-brainer, extremely simple to use, and requires no infrastructure.


| Factor                     | Memoization                               | Caching                                |
|----------------------------|-------------------------------------------|----------------------------------------|
| **Scope**                  | Local to a single class instance within the current process.  | Either local or shared, when run as an external service such as Redis. |
| **Unicity of cache items** | Specific to the current instance or type. | Based on explicit `string` cache keys. |
| **Complexity & overhead**  | Minimal overhead.                         | Significant overhead related to the generation of cache keys and, in case of distributed caching, serialization.  |
| **Expiration & invalidation** | No expiration or invalidation.      | Advanced and configurable expiration policies and invalidation APIs.  |

> [!div class="see-also"]
> <xref:patterns>
> <xref:caching>



