// This is public domain Metalama sample code.

using System;

namespace Doc.RetryShippingCalls;

internal class Program
{
    private static void Main()
    {
        var service = new ShipmentService();

        Console.WriteLine( $"Cost: {service.EstimateCost( "US" )}" );
        Console.WriteLine(
            $"Status: {service.GetStatusAsync( "TRK-1" ).GetAwaiter().GetResult()}" );
    }
}
