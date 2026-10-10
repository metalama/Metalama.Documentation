// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.LAMA0259.Fixed;

internal class OrderService
{
    private readonly TextWriter _log = Console.Out;

    [Log]
    public void PlaceOrder( string product ) => Console.WriteLine( $"Ordered {product}." );
}
