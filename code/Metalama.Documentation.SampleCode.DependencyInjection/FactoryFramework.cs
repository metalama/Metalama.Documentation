// This is public domain Metalama sample code.

using System;

namespace Doc.FactoryFramework;

// A class using the logging aspect.
// The factory framework will pull IServiceFactory<ILogger> and call Create().
public partial class Greeter
{
    [Log]
    public void SayHello() => Console.WriteLine( "Hello!" );
}
