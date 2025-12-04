// This is public domain Metalama sample code.

using System;

namespace Doc.TransitiveFabric;

// This project references the assembly containing LoggingPolicyFabric.
// The transitive fabric automatically applies logging to all public methods.

public class OrderService
{
    public void PlaceOrder( string orderId )
    {
        Console.WriteLine( $"Processing order {orderId}." );
    }

    public void CancelOrder( string orderId )
    {
        Console.WriteLine( $"Cancelling order {orderId}." );
    }

    private void ValidateOrder( string orderId )
    {
        // Private methods are not logged.
    }
}
