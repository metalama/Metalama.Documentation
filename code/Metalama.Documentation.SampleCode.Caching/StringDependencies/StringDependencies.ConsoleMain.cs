// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using Xunit;

namespace Doc.StringDependencies;

public sealed class ConsoleMain( ProductCatalogue catalogue ) : IConsoleMain
{
    private void PrintCatalogue()
    {
        var products = catalogue.GetProducts();

        foreach ( var product in products )
        {
            var price = catalogue.GetPrice( product );
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

        // Adding a product and updating the price.
        Console.WriteLine( "Updating the catalogue." );
        catalogue.AddProduct( "wheat", 150 );
        catalogue.UpdatePrice( "corn", 110 );

        // Read the catalogue a third time.
        Assert.Equal( 2, catalogue.GetProducts().Length );
        Assert.Equal( 110, catalogue.GetPrice( "corn" ) );

        // Print the catalogue.
        Console.WriteLine( "Catalogue after changes:" );
        this.PrintCatalogue();
    }
}