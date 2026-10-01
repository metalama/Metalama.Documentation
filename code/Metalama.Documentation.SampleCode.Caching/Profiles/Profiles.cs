// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.Profiles;

public sealed class ProductCatalogue
{
    public int OperationCount { get; private set; }

    [Cache( ProfileName = "Hot" )]
    public decimal GetPrice( string productId )
    {
        Console.WriteLine( "Getting the price from database." );
        this.OperationCount++;

        return 100 + this.OperationCount;
    }

    [Cache]
    public string[] GetProducts()
    {
        Console.WriteLine( "Getting the product list from database." );

        this.OperationCount++;

        return new[] { "corn" };
    }
}