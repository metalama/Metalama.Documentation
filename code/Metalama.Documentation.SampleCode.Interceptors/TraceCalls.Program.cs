// This is public domain Metalama sample code.

using System;

namespace Doc.TraceCalls;

internal class Program
{
    private static void Main()
    {
        var cart = new Cart();
        cart.SetCountry( "US" );
        cart.Add( "BOOK", 2 );

        try
        {
            cart.Add( "PEN", 1 );
        }
        catch ( ArgumentException )
        {
            // The trace shows the failed call.
        }
    }
}
