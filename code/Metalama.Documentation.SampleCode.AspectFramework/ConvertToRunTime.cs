// This is public domain Metalama sample code.

using System;

namespace Doc.ConvertToRunTime;

internal class Foo
{
    [ConvertToRunTimeAspect]
    private void Bar( string a, int c, DateTime e )
    {
        Console.WriteLine( $"Method({a}, {c}, {e})" );
    }
}