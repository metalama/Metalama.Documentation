// This is public domain Metalama sample code.

using System;

namespace Doc.GettingStarted_Fabric;

internal class Foo
{
    [Log]
    public void Method1()
    {
        Console.WriteLine( "Hello, world." );
    }
}