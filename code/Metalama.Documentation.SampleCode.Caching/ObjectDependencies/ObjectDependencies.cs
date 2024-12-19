// This is public domain Metalama sample code.

#if METALAMA
using Metalama.Patterns.Caching;
#endif

using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.ObjectDependencies;

internal static class GlobalDependencies
{
    public static ICacheDependency ProductCatalogue =
        new StringDependency( nameof(ProductCatalogue) );

    public static ICacheDependency ProductList = new StringDependency( nameof(ProductList) );
}

public record Product( string Name, decimal Price ) : ICacheDependency
{
    string ICacheDependency.GetCacheKey( ICachingService cachingService ) => this.Name;

    // Means that when we invalidate the current product in cache, we should also invalidate the product catalogue.
    IReadOnlyCollection<ICacheDependency> ICacheDependency.CascadeDependencies { get; } =
        new[] { GlobalDependencies.ProductCatalogue };
}

public sealed partial class ProductCatalogue
{
    private readonly Dictionary<string, Product> _dbSimulator =
        new() { ["corn"] = new Product( "corn", 100 ) };

    public int DbOperationCount { get; private set; }

    [Cache]
    public Product GetProduct( string productId )
    {
        Console.WriteLine( $"Getting the price of {productId} from database." );
        this.DbOperationCount++;

        var product = this._dbSimulator[productId];

#if METALAMA
        // [<snippet AddDependency>]
        this._cachingService.AddDependency( product );  
        // [<endsnippet AddDependency>]
#endif
        return product;
    }

    [Cache]
    public string[] GetProducts()
    {
        Console.WriteLine( "Getting the product list from database." );

        this.DbOperationCount++;

#if METALAMA
            this._cachingService.AddDependency( GlobalDependencies.ProductList );
#endif

        return this._dbSimulator.Keys.ToArray();
    }

    [Cache]
    public IReadOnlyCollection<Product> GetPriceList()
    {
        this.DbOperationCount++;

#if METALAMA
            this._cachingService.AddDependency( GlobalDependencies.ProductCatalogue );
#endif

        return this._dbSimulator.Values;
    }

    public void AddProduct( Product product )
    {
        Console.WriteLine( $"Adding the product {product.Name}." );

        this.DbOperationCount++;

        this._dbSimulator.Add( product.Name, product );

#if METALAMA
            this._cachingService.Invalidate( product );
            this._cachingService.Invalidate( GlobalDependencies.ProductList );
#endif
    }

    public void UpdateProduct( Product product )
    {
        if ( !this._dbSimulator.ContainsKey( product.Name ) )
        {
            throw new KeyNotFoundException();
        }

        Console.WriteLine( $"Updating the price of {product.Name}." );

        this.DbOperationCount++;
        this._dbSimulator[product.Name] = product;

#if METALAMA
            // [<snippet Invalidate>]
            this._cachingService.Invalidate( product  ); 
                                                                                            
                                                                                            // [<endsnippet Invalidate>]
#endif
    }
}