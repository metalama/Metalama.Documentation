// This is public domain Metalama sample code.

using System;
using Metalama.Documentation.Helpers.ConsoleApp;
using System.Threading.Tasks;

namespace Doc.AzureSynchronized;

public sealed class ConsoleMain : IAsyncConsoleMain
{
    private readonly ProductCatalogue _productCatalogue;
    private readonly string _appName;

    public ConsoleMain( ProductCatalogue productCatalogue, IConsoleHost host )
    {
        this._productCatalogue = productCatalogue;
        this._appName = host.Arguments[0];
    }

    public async Task ExecuteAsync()
    {
        // Force running in parallel.
        await Task.Yield();

        for ( var i = 0; i < 3; i++ )
        {
            for ( var j = 0; j < 3; j++ )
            {
                // Getting the product.
                var corn = this._productCatalogue.GetProduct( "corn" );
                Console.WriteLine( $"{this._appName} reads and gets {corn}." );
                await Task.Delay( 20 + Random.Shared.Next( 4 ) );
            }

            // Updating the product.
            var updatedCorn = new Product(
                "corn",
                100 + Random.Shared.Next( 20 ),
                $"Updated by {this._appName}, i={i}" );

            Console.WriteLine( $"{this._appName} update {updatedCorn}." );
            this._productCatalogue.Update( updatedCorn );
        }

        Console.WriteLine(
            $"In total, CloudCalculator in {this._appName} performed {this._productCatalogue.DbOperationCount} database operation(s)." );
    }
}