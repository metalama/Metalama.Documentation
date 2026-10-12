// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;

namespace Doc.ValueAdapter;

public sealed class ConsoleMain : IConsoleMain
{
    private readonly ProductCatalogue _catalogue;

    public ConsoleMain( ProductCatalogue catalogue )
    {
        this._catalogue = catalogue;
    }

    public void Execute()
    {
        for ( var i = 0; i < 2; i++ )
        {
            // Get the StringBuilder through the cache.
            var products = this._catalogue.GetProductsAsStringBuilder();

            // Modify the StringBuilder. Without the ValueAdapter, we would receive the
            // mutated instance and we would have our prefix twice.
            products.Insert( 0, "The list of products is: " );

            // Print.
            Console.WriteLine( products );
        }
    }
}