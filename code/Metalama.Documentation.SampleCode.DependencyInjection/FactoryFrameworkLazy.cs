// This is public domain Metalama sample code.

using System;

namespace Doc.FactoryFrameworkLazy;

// A class using the lazy logging aspect.
// The factory framework will lazily call Create() on first property access.
public partial class LazyGreeter
{
    [LogLazy]
    public void SayHello() => Console.WriteLine( "Hello!" );
}
