// This is public domain Metalama sample code.

using System;

namespace Doc.MethodGroups;

public static class TextLogging
{
    // A method group is intercepted only by a method with exactly the same signature.
    public static string Normalize( string text )
    {
        Console.WriteLine( $"Normalizing '{text}'." );

        return TextUtil.Normalize( text );
    }
}
