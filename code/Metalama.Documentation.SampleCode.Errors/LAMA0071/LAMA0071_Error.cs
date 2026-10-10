// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0071.Error;

internal class OrderService
{
    [Audit]
    public void PlaceOrder( int productId ) { }

    public void WriteAudit<TCategory>( string operation )
    {
        Console.WriteLine( $"[{typeof(TCategory).Name}] {operation}" );
    }
}
