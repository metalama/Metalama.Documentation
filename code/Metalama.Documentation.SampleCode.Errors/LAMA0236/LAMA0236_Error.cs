// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0236.Error;

public class Logger
{
    public void Write( string message ) => Console.WriteLine( message );
}

internal partial class OrderService
{
    [Log]
    public void PlaceOrder( int orderId ) { }
}
