// This is public domain Metalama sample code.

using System;

namespace Doc.CompareTaxCalculators;

internal class Program
{
    private static void Main()
    {
        var service = new InvoiceService();

        foreach ( var region in new[] { "EU", "US", "CH" } )
        {
            Console.WriteLine( $"Total in {region}: {service.ComputeTotal( 100, region )}" );
        }
    }
}
