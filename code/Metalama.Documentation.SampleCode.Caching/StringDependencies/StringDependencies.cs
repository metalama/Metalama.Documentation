// This is public domain Metalama sample code.

#if METALAMA
using Metalama.Patterns.Caching;
#endif

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Doc.StringDependencies;

public sealed partial class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _dbSimulator = new() { ["corn"] = 100 };

    public int DbOperationCount { get; private set; }

    [Cache]
    public decimal GetPrice( string productId )
    {
        Console.WriteLine( $"Getting the price of {productId} from database." );
        this.DbOperationCount++;

#if METALAMA
         // [<snippet AddDependency>]
        this._cachingService.AddDependency( $"ProductPrice:{productId}" );  
        // [<endsnippet AddDependency>]
#endif
        return this._dbSimulator[productId];
    }

    [Cache]
    public string[] GetProducts()
    {
        Console.WriteLine( "Getting the product list from database." );

        this.DbOperationCount++;

#if METALAMA
            this._cachingService.AddDependency( "ProductList" );
#endif

        return this._dbSimulator.Keys.ToArray();
    }

    [Cache]
    public ImmutableDictionary<string, decimal> GetPriceList()
    {
        this.DbOperationCount++;

#if METALAMA
            this._cachingService.AddDependency( "PriceList" );
#endif

        return this._dbSimulator.ToImmutableDictionary();
    }

    public void AddProduct( string productId, decimal price )
    {
        Console.WriteLine( $"Adding the product {productId}." );

        this.DbOperationCount++;
        this._dbSimulator.Add( productId, price );

#if METALAMA
            this._cachingService.Invalidate( "ProductList", "PriceList" );
#endif
    }

    public void UpdatePrice( string productId, decimal price )
    {
        if ( !this._dbSimulator.ContainsKey( productId ) )
        {
            throw new KeyNotFoundException();
        }

        Console.WriteLine( $"Updating the price of {productId}." );

        this.DbOperationCount++;
        this._dbSimulator[productId] = price;

#if METALAMA
            // [<snippet Invalidate>]
            this._cachingService.Invalidate( $"ProductPrice:{productId}", "PriceList"  ); 
            // [<endsnippet Invalidate>]
#endif
    }
}