// This is public domain Metalama sample code.

#if METALAMA
using Metalama.Patterns.Caching;
#endif

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.ImperativeInvalidate;

public sealed partial class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _dbSimulator = new() { ["corn"] = 100 };

    public int DbOperationCount { get; private set; }

    // [<snippet Cache>]
    [Cache]
    public decimal GetPrice( string productId )

        // [<endsnippet Cache>]
    {
        Console.WriteLine( $"Getting the price of {productId} from database." );
        this.DbOperationCount++;

        return this._dbSimulator[productId];
    }

    [Cache]
    public string[] GetProducts()
    {
        Console.WriteLine( "Getting the product list from database." );

        this.DbOperationCount++;

        return this._dbSimulator.Keys.ToArray();
    }

    public void AddProduct( string productId, decimal price )
    {
        Console.WriteLine( $"Adding the product {productId}." );

        this.DbOperationCount++;
        this._dbSimulator.Add( productId, price );

#if METALAMA
            this._cachingService.Invalidate( this.GetProducts );

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
            // [<snippet InvalidateCache>]
            this._cachingService.Invalidate( this.GetPrice, productId ); 
                                                                         
                                                                         // [<endsnippet InvalidateCache>]
#endif
    }
}