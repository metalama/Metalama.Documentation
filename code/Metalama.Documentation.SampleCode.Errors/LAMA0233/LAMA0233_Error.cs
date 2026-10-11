// This is public domain Metalama sample code.

namespace Doc.LAMA0233.Error;

internal class OrderService
{
    [Log]
    public void PlaceOrder( int orderId ) { }

    [Log]
    public static void PurgeOrders() { }
}
