// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Extensions.DependencyInjection;
using System;

namespace Doc.ServiceLocatorExample;

// With the ServiceLocator framework, dependencies are resolved from a global IServiceProvider
// instead of being pulled through constructor parameters.
public class MyService : IConsoleMain
{
    [Dependency]
    private IMessageWriter _messageWriter;

    public void Execute()
    {
        this._messageWriter.Write( "Hello from ServiceLocator!" );
    }
}

public interface IMessageWriter
{
    void Write( string message );
}

public class ConsoleMessageWriter : IMessageWriter
{
    public void Write( string message ) => Console.WriteLine( message );
}
