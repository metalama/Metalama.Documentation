// This is public domain Metalama sample code.

using System;

namespace Doc.ImportAttributeFramework;

// A class using the aspect.
public partial class Greeter
{
    [Log]
    public void SayHello() => Console.WriteLine( "Hello!" );
}
