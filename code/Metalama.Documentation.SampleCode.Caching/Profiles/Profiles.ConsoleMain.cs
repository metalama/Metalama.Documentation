// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using System.Threading;

namespace Doc.Profiles;

public sealed class ConsoleMain : IConsoleMain
{
    private readonly ProductCatalogue _catalogue;

    public ConsoleMain( ProductCatalogue catalogue )
    {
        this._catalogue = catalogue;
    }

    public void Execute()
    {
        for ( var i = 0; i < 5; i++ )
        {
            Console.WriteLine( "Printing all prices..." );

            var products = this._catalogue.GetProducts();

            foreach ( var product in products )
            {
                var price = this._catalogue.GetPrice( product );
                Console.WriteLine( $"Price of '{product}' is {price}." );

                Thread.Sleep( 150 );
            }
        }

        Console.WriteLine(
            $"ProductCatalogue performed {this._catalogue.OperationCount} operations in total." );
    }
}