// This is public domain Metalama sample code.

namespace Doc.LAMA0535.Fixed;

// Fixed: the class is partial.
[InitializationHook]
internal partial class OrderService
{
    public void PlaceOrder( int orderId ) { }
}
