// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doc.ValueAdapter;

public sealed class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _dbSimulator = new() { ["corn"] = 100 };

    public int DbOperationCount { get; private set; }

    // Very weird API but suppose it's legacy and we need to keep it, but cache it.
    [Cache]
    public StringBuilder GetProductsAsStringBuilder()
    {
        Console.WriteLine( "Getting the product list from database." );

        this.DbOperationCount++;

        var stringBuilder = new StringBuilder();

        foreach ( var productId in this._dbSimulator.Keys )
        {
            if ( stringBuilder.Length > 0 )
            {
                stringBuilder.Append( "," );
            }

            stringBuilder.Append( productId );
        }

        return stringBuilder;
    }
}