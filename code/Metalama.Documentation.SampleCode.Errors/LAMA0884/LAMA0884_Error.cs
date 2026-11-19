// This is public domain Metalama sample code.

namespace Doc.LAMA0884.Error;

internal class OrderService
{
    [Log( "Orders", "Billing" )]
    public void PlaceOrder( string product, int quantity )
    {
        // Places the order.
    }
}
