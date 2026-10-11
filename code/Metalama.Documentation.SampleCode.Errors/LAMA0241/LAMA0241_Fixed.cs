// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.LAMA0241.Fixed;

internal class OrderService
{
    public TextWriter? Logger { get; set; } = Console.Out;

    [Log]
    public void PlaceOrder( int quantity ) { }
}
