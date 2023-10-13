---
uid: caching-value-adapters
---
# Caching mutable or stream-like types with value adapters

In theory, you should cache only _immutable_ types. In practice, there are some problematic types that we still want to cache:

* Stream-like types like the <xref:System.Collections.Generic.IEnumerator`1> interface or the <xref:System.IO.Stream> class cannot be directly cached because the position of the enumerator or stream can be changed by the caller. 
* Interfaces like <xref:System.Collections.Generic.IEnumerable`1> cannot be cached because the real value may be a query instead of the _result_ of this query, and would be useless to cache. 
* Some types, like <xref:System.Collections.Generic.List`1> or arrays, and mutable, and the caller may modify the instance stored in cache.

How can we cache these problematic types in a safe manner?

Metalama addresses this problem by the concept of a *value adapter*. A value adapter allows you to store another type than the one than the return type of the cached method. The method return value is called the *exposed value* because this is the value exposed by your API. The exposed value must be type-compatible with the method return type. The value that is actually stored in cache is called the *stored value*. For instance, for a method returning a <xref:System.IO.Stream>, the stored value is an array of bytes and the exposed value is a <xref:System.IO.MemoryStream>. 


## Standard value adapters

The following value adapters are used automatically by default:

| Return type | Stored type | Exposed type | Comments |
|-------------------------------------------------|-------------|--------------|----------|
| <xref:System.Collections.Generic.IEnumerable`1> | <xref:System.Collections.Generic.List`1> | <xref:System.Collections.Generic.List`1> |  |
| <xref:System.Collections.Generic.IEnumerator`1> | <xref:System.Collections.Generic.List`1> | <xref:System.Collections.Generic.List`1.Enumerator> | The <xref:System.Collections.IEnumerator.Reset> method is not supported by the exposed value.  |
| <xref:System.IO.Stream> | <xref:System.Byte> []  | <xref:System.IO.MemoryStream> |  |


## Implementing a custom value adapter


To implement a custom value adapter:

1. Create a class implementing the <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter`1> interface or the non-generic <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter> interface. 

2. Go back to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*?text=serviceCollection.AddCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.AddValueAdapter*> method, then pass an instance of your <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter`1> .


    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/ValueAdapter/ValueAdapter.Program.cs marker="AddCaching"]

> [!NOTE]
> Null values are handled automatically outside of the value adapters.

### Example: caching a StringBuilder

Suppose you are maintaining a legacy service that implements the weird practice of returning a `StringBuilder` instead of `string`. You are in charge of improving the performance of this API, so you want to cache the result of this method. However, you cannot cache mutable objects, since it would mean that, if a caller modifies the `StringBuilder` , the next caller would receive the modified copy. Therefore, you decide to cache the `string` instead of the `StringBuilder`, and return a new `StringBuilder` every time the value is fetched from cache.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ValueAdapter/ValueAdapter.cs]