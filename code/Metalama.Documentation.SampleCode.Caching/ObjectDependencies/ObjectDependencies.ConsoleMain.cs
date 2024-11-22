// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using Xunit;

namespace Doc.ObjectDependencies;

public sealed class ConsoleMain( ProductCatalogue catalogue ) : IConsoleMain
{
    private void PrintCatalogue()
    {
        var products = catalogue.GetProducts();

        foreach ( var product in products )
        {
            var price = catalogue.GetProduct( product );
            Console.WriteLine( $"Price of '{product}' is {price}." );
        }
    }

    public void Execute()
    {
        Console.WriteLine( "Read the price catalogue a first time." );
        this.PrintCatalogue();

        Console.WriteLine(
            "Read the price catalogue a second time time. It should be completely performed from cache." );

        var operationsBefore = catalogue.DbOperationCount;
        this.PrintCatalogue();
        var operationsAfter = catalogue.DbOperationCount;
        Assert.Equal( operationsBefore, operationsAfter );

        // There should be just one product in the catalogue.
        Assert.Single( catalogue.GetProducts() );

        var corn = catalogue.GetProduct( "corn" );

        // Adding a product and updating the price.
        Console.WriteLine( "Updating the catalogue." );

        catalogue.AddProduct( new Product( "wheat", 150 ) );
        catalogue.UpdateProduct( corn with { Price = 110 } );

        // Read the catalogue a third time.
        Assert.Equal( 2, catalogue.GetProducts().Length );
        Assert.Equal( 110, catalogue.GetProduct( "corn" ).Price );

        // Print the catalogue.
        Console.WriteLine( "Catalogue after changes:" );
        this.PrintCatalogue();
    }
}