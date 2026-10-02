// This is public domain Metalama sample code.

using System;

namespace Doc.MixedInitialization;

internal class Program
{
    private static void Main()
    {
        var customer = new Customer( 1 ) { FirstName = "Alice", LastName = "Smith" };

        Console.WriteLine( $"State: {LifecycleRegistry.GetState( customer )}" );
    }
}
