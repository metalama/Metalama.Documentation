// This is public domain Metalama sample code.

using System;

namespace Doc.SecondaryInstancesWarning;

public class OrderService
{
    // This method only has [Log] from the fabric - no warning.
    public void PlaceOrder( string productId, int quantity )
    {
        Console.WriteLine( "Placing order." );
    }

    // This method has [Log] from both the attribute and the fabric.
    // A warning will be reported about the duplicate.
    [Log]
    public void GetOrderStatus( string orderId )
    {
        Console.WriteLine( "Getting order status." );
    }
}
