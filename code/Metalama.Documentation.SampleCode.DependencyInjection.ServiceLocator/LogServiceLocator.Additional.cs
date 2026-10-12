// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Doc.LogServiceLocator;

// The program entry point.
public static class Program
{
    private static Task Main()
    {
        // Creates a service collection, add the service, and build a service provider.
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IMessageWriter>( new MessageWriter() );
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assigns the service provider to the global service locator.
        ServiceProviderProvider.ServiceProvider = () => serviceProvider;

        // Executes the program.
        return new Worker().ExecuteAsync();
    }
}

// Definition of the interface consumed by the aspect.
public interface IMessageWriter
{
    void Write( string message );
}

// Implementation of the interface actually used by the aspect.
public class MessageWriter : IMessageWriter
{
    public void Write( string message )
    {
        Console.WriteLine( message );
    }
}