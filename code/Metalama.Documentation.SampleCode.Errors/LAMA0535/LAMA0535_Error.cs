// This is public domain Metalama sample code.

namespace Doc.LAMA0535.Error;

// Error: the class isn't partial, so the aspect can't introduce a partial method into it.
[InitializationHook]
internal class OrderService
{
    public void PlaceOrder( int orderId ) { }
}
