// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0012.Error;

internal class OrderService
{
    [Audit]
    public void PlaceOrder( string product, int quantity )
    {
        Console.WriteLine( $"Placing an order for {quantity} {product}." );
    }

    private void WriteAudit( string typeName, string methodName )
    {
        Console.WriteLine( $"Audit: {typeName}.{methodName}" );
    }
}
