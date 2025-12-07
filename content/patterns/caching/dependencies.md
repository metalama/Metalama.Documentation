---
uid: caching-dependencies
summary: "The document explains the concept of cache dependencies in Metalama Caching, detailing how to add string dependencies, implement object-oriented dependencies, and suspend the collection of cache dependencies. It also highlights the potential trade-offs in performance and resource usage."
keywords: "cache dependencies, Metalama Caching, string dependencies, ICacheDependency interface, cache invalidation, object-oriented cache dependencies, ICachingService, caching backend, AddDependency method, Invalidate method"
created-date: 2024-06-08
modified-date: 2025-11-30
---
# Working with cache dependencies

Cache dependencies serve two primary purposes. Firstly, they act as an intermediary layer between cached methods (typically read methods) and invalidating methods (typically write methods), thereby reducing the coupling between these methods. Secondly, cache dependencies can represent external dependencies, such as file system dependencies or SQL dependencies.

Compared to direct invalidation, the use of dependencies results in lower performance and increased resource consumption in the caching backend due to the need to store and synchronize the graph of dependencies. For more details on direct invalidation, refer to <xref:caching-invalidation>.

## Adding string dependencies

All dependencies are eventually represented as strings. Although we recommend using one of the strongly-typed methods mentioned below, it's beneficial to understand how string dependencies operate.

To add or invalidate dependencies, you'll typically access the <xref:Metalama.Patterns.Caching.ICachingService> interface. If you're using dependency injection, first declare your class as `partial`, and the interface is available under a field named `_cachingService`. Otherwise, use the <xref:Metalama.Patterns.Caching.CachingService.Default?CachingService.Default> property.

Within _read_ methods, use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.AddDependency*?text=ICachingService.AddDependency*> at any time to add a dependency to the method being executed, for the arguments with which it is executed. You can pass an arbitrary `string` to this method, potentially including the method arguments.

For instance, here is how to add a `string` dependency:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/StringDependencies/StringDependencies.cs marker="AddDependency"]

In the _update_ methods, use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.AddDependency*?text=ICachingService.Invalidate*> method and pass the dependency `string` to remove any cache item that has a dependency on this `string`.

For instance, the following line invalidates two `string` dependencies:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/StringDependencies/StringDependencies.cs marker="Invalidate"]

> [!NOTE]
> Dependencies function correctly with recursive method calls. If a cached method `A` calls another cached method `B`, all dependencies of `B` automatically become dependencies of `A`, even if `A` was cached when `A` was being evaluated.

### Example: string dependencies

The following code is a variation of our `ProductCatalogue` example. It has three read methods:

* `GetPrice` returns the price of a given product,
* `GetProducts` returns a list of products without their prices, and
* `GetPriceList` returns both the name and the price of all products.

It has two write methods:

* `AddProduct` adds a product, therefore it should affect both `GetProducts` and  `GetPriceList`, and
* `UpdatePrice` changes the price of a given product, and should affect `GetPrice` for this product and `GetPriceList`.

We model the dependencies using three string templates:

* `ProductList` represents the product list without prices,
* `ProductPrice:{productId}` represents the price of a given product, and
* `PriceList` represents the complete price list.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/StringDependencies/StringDependencies.cs]

## Adding object-oriented dependencies through the ICacheDependency interface

As previously mentioned, working with string dependencies can be error-prone as the code generating the string is duplicated in both the _read_ and the _write_ methods. A more efficient approach is to encapsulate the cache key generation logic, i.e., represent the cache dependency as an object and add some key-generation logic to this object.

For this reason, Metalama Caching allows you to work with strongly-typed, object-oriented dependencies through the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> interface.

This interface has two members:

* <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency.GetCacheKey*> should return the `string` representation of the caching key,
* <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency.CascadeDependencies>, an optional property, can return a list of dependencies that should be recursively invalidated when the current dependency is invalidated.

How and where you implement <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> is entirely up to you. You have the following options:

1. The most practical option is often to implement the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> in your domain objects.
2. Alternatively, create a parallel object model implementing <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> &mdash; just to represent dependencies.
3. If you have types that can already be used in cache keys, e.g., thanks to the <xref:Metalama.Patterns.Caching.Aspects.CacheKeyAttribute?text=[CacheKey]> aspect or another mechanism (see <xref:caching-keys>), you can turn these objects into dependencies by wrapping them into an <xref:Metalama.Patterns.Caching.Dependencies.ObjectDependency>. You can also use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.AddObjectDependency*> and <xref:Metalama.Patterns.Caching.CachingServiceExtensions.InvalidateObject*> methods to avoid creating a wrapper.
4. To represent singleton dependencies, it can be convenient to assign them a constant string and wrap this string into a <xref:Metalama.Patterns.Caching.Dependencies.StringDependency> object.

### Example: object-oriented dependencies

Let's revamp our previous example using object-oriented dependencies.

Instead of just working with primitive types like `string` and `decimal`, we create a new type `record Product( string Name, decimal Price)` and make this type implement the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> interface.
To represent dependencies of the global collections `ProductList` and `PriceList`, we use instances of the <xref:Metalama.Patterns.Caching.Dependencies.StringDependency> class rather than creating new classes for each. These instances are exposed as static properties of the `GlobalDependencies` static class.

To ensure the entire `PriceList` is invalidated whenever a `Product` is updated, we return the global `PriceList` dependency instance from the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency.CascadeDependencies> property of the `Product` class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ObjectDependencies/ObjectDependencies.cs]

## Suspending the collection of cache dependencies

A new caching context is created for each cached method. The caching context is propagated along all invoked methods and is implemented using <xref:System.Threading.AsyncLocal`1>.

When a parent cached method calls a child cached method, the dependencies of the child methods are automatically added to the parent method, even if the child method wasn't executed because its result was found in the cache. Therefore, invalidating a child method automatically invalidates the parent method, which is often an intuitive and desirable behavior.

However, there are cases where propagating the caching context from the parent to the child methods (and thereby the collection of child dependencies into the parent context) isn't desirable. For instance, if the parent method runs an asynchronous child task using `Task.Run` and doesn't wait for its completion, then the dependencies of methods called in the child task probably shouldn't be propagated to the parent. This is because the child task could be considered a side effect of the parent method and shouldn't affect caching. Undesired dependencies wouldn't compromise the program's correctness, but they would make it less efficient.

To suspend the collection of dependencies in the current context and in all child contexts, use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.SuspendDependencyPropagation*?text=_cachingService.SuspendDependencyPropagation> method within a `using` construct.

> [!div class="see-also"]
> <xref:caching>
> <xref:caching-invalidation>
> <xref:caching-redis>
