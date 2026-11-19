// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0890.Error;

[Log]
internal class OrderService : IDisposable
{
    public void PlaceOrder( string product, int quantity )
    {
        // Places the order.
    }

    public void Dispose() { }
}
