// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @DisableCompareProgramOutput -- the output is random
#endif

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#if METALAMA
using Metalama.Patterns.Caching;
#endif

namespace Doc.AzureSynchronized;

public record Product( string Id, decimal Price, string? Remarks = null );

public sealed partial class ProductCatalogue
{
    // This instance is intentionally shared between both app instances to simulate
    // a shared database.
    private static readonly ConcurrentDictionary<string, Product> _dbSimulator
        = new() { ["corn"] = new Product( "corn", 100, "Initial record." ) };

    public int DbOperationCount { get; private set; }

    [Cache]
    public Product GetProduct( string productId )
    {
        Console.WriteLine( $"Getting the product of {productId} from database." );

        this.DbOperationCount++;

        return _dbSimulator[productId];
    }

    public void Update( Product product )
    {
        if ( !_dbSimulator.ContainsKey( product.Id ) )
        {
            throw new KeyNotFoundException();
        }

        Console.WriteLine( $"Updating the product {product.Id}." );

        this.DbOperationCount++;
        _dbSimulator[product.Id] = product;

#if METALAMA
            this._cachingService.Invalidate( this.GetProduct, product.Id );
#endif
    }
}