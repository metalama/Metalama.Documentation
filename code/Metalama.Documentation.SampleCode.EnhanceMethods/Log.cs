// This is public domain Metalama sample code.

using System;

namespace Doc.Logging;

public class Program
{
    [Log]
    public static void SayHello( string name )
    {
        Console.WriteLine( $"Hello {name}" );
    }

    public static void Main()
    {
        SayHello( "Your Majesty" );
    }
}