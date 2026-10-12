// This is public domain Metalama sample code.

using System;
using System.Threading.Tasks;

namespace Doc.LogServiceLocator;

// The class using the Log aspect. This class is NOT instantiated by any dependency injection container.
public class Worker
{
    [Log]
    public Task ExecuteAsync()
    {
        Console.WriteLine( "Hello, world." );

        return Task.CompletedTask;
    }
}