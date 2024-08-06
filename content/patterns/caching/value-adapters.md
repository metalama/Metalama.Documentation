---
uid: caching-value-adapters
summary: "The document discusses the concept of value adapters in Metalama for caching mutable or stream-like types. It details how to implement a custom value adapter and provides examples."
---
# Caching mutable or stream-like types with value adapters

In theory, only _immutable_ types should be cached. However, in practice, there are some problematic types that we might still want to cache:

* Stream-like types such as the <xref:System.Collections.Generic.IEnumerator`1> interface or the <xref:System.IO.Stream> class cannot be directly cached because the position of the enumerator or stream can be altered by the caller.
* Interfaces like <xref:System.Collections.Generic.IEnumerable`1> cannot be cached because the actual value might be a query rather than the _result_ of this query, which would be pointless to cache.
* Some types, like <xref:System.Collections.Generic.List`1> or arrays, are mutable, and the caller may modify the instance stored in the cache.

So, how can we safely cache these problematic types?

Metalama addresses this issue through the concept of a *value adapter*. A value adapter allows you to store a different type than the one returned by the cached method. The method return value is referred to as the *exposed value* because this is the value exposed by your API. The exposed value must be type-compatible with the method return type. The value that is actually stored in the cache is called the *stored value*. For instance, for a method returning a <xref:System.IO.Stream>, the stored value is an array of bytes and the exposed value is a <xref:System.IO.MemoryStream>.


## Standard value adapters

By default, the following value adapters are used automatically:

| Return type | Stored type | Exposed type | Comments |
|-------------------------------------------------|-------------|--------------|----------|
| <xref:System.Collections.Generic.IEnumerable`1> | <xref:System.Collections.Generic.List`1> | <xref:System.Collections.Generic.List`1> |  |
| <xref:System.Collections.Generic.IEnumerator`1> | <xref:System.Collections.Generic.List`1> | <xref:System.Collections.Generic.List`1.Enumerator> | The <xref:System.Collections.IEnumerator.Reset> method is not supported by the exposed value.  |
| <xref:System.IO.Stream> | <xref:System.Byte> []  | <xref:System.IO.MemoryStream> |  |


## Implementing a custom value adapter


To implement a custom value adapter:

1. Create a class implementing the <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter`1> interface or the non-generic <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter> interface.

2. Go back to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddMetalamaCaching*?text=serviceCollection.AddMetalamaCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.AddValueAdapter*> method, then pass an instance of your <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter`1>.


    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/ValueAdapter/ValueAdapter.Program.cs marker="AddMetalamaCaching"]

> [!NOTE]
> Null values are automatically handled outside of the value adapters.

### Example: Caching a StringBuilder

Let's say you are maintaining a legacy service that implements the unusual practice of returning a `StringBuilder` instead of a `string`. You are responsible for improving the performance of this API, so you want to cache the result of this method. However, you cannot cache mutable objects, as this would mean that if a caller modifies the `StringBuilder`, the next caller would receive the modified copy. Therefore, you decide to cache the `string` instead of the `StringBuilder`, and return a new `StringBuilder` every time the value is fetched from the cache.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ValueAdapter/ValueAdapter.cs]

