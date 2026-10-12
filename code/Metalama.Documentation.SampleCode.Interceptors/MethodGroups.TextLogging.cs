// This is public domain Metalama sample code.

using System;

namespace Doc.MethodGroups;

public static class TextLogging
{
    // A method group is intercepted only by a method with exactly the same signature.
    // [<snippet Signature>]
    public static string Normalize( string text )
    // [<endsnippet Signature>]
    {
        Console.WriteLine( $"Normalizing '{text}'." );

        return TextUtil.Normalize( text );
    }
}
