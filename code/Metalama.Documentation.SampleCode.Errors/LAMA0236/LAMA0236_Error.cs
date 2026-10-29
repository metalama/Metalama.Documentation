// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The Logger field also violates the immutability contract, which is not the topic of this sample.
// @IgnoredDiagnostic(LAMA0882)
#endif

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
