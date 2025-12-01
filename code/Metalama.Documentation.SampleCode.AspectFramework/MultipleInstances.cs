// This is public domain Metalama sample code.

using System;

namespace Doc.MultipleInstances;

public class OrderService
{
    // This method has [Log] from both the custom attribute (Category="Orders")
    // and from the fabric (Category="Monitoring"). The aspect merges both categories.
    [Log( Category = "Orders" )]
    public void PlaceOrder( string productId, int quantity )
    {
        Console.WriteLine( "Business logic here." );
    }

    // This method only has [Log] from the fabric (default Category="Monitoring").
    public void GetOrderStatus( string orderId )
    {
        Console.WriteLine( "Business logic here." );
    }
}
