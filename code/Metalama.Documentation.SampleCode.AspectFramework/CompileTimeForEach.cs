// This is public domain Metalama sample code.

using System;

namespace Doc.CompileTimeForEach;

internal class Foo
{
    [CompileTimeForEach]
    private void Bar( int a, string b )
    {
        Console.WriteLine( $"Hello, world! a={a}, b='{b}'." );
    }
}