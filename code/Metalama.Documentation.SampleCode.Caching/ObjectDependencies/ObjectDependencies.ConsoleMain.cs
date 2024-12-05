// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using Xunit;

namespace Doc.ObjectDependencies;

public sealed class ConsoleMain : IConsoleMain
{
    private readonly ProductCatalogue _catalogue;

    public ConsoleMain( ProductCatalogue catalogue )
    {
        this._catalogue = catalogue;
    }

    private void PrintCatalogue()
    {
        var products = this._catalogue.GetProducts();

        foreach ( var product in products )
        {
            var price = this._catalogue.GetProduct( product );
            Console.WriteLine( $"Price of '{product}' is {price}." );
        }
    }

    public void Execute()
    {
        Console.WriteLine( "Read the price catalogue a first time." );
        this.PrintCatalogue();

        Console.WriteLine(
            "Read the price catalogue a second time time. It should be completely performed from cache." );

        var operationsBefore = this._catalogue.DbOperationCount;
        this.PrintCatalogue();
        var operationsAfter = this._catalogue.DbOperationCount;
        Assert.Equal( operationsBefore, operationsAfter );

        // There should be just one product in the catalogue.
        Assert.Single( this._catalogue.GetProducts() );

        var corn = this._catalogue.GetProduct( "corn" );

        // Adding a product and updating the price.
        Console.WriteLine( "Updating the catalogue." );

        this._catalogue.AddProduct( new Product( "wheat", 150 ) );
        this._catalogue.UpdateProduct( corn with { Price = 110 } );

        // Read the catalogue a third time.
        Assert.Equal( 2, this._catalogue.GetProducts().Length );
        Assert.Equal( 110, this._catalogue.GetProduct( "corn" ).Price );

        // Print the catalogue.
        Console.WriteLine( "Catalogue after changes:" );
        this.PrintCatalogue();
    }
}