// This is public domain Metalama sample code.

using System;

namespace Doc.SimpleLogging;

internal class Foo
{
    [SimpleLog]
    public void Method1()
    {
        Console.WriteLine( "Hello, world." );
    }
}