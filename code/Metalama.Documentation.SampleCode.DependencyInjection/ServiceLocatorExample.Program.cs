// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.ServiceLocatorExample;

// This class shows how to configure the ServiceProviderProvider with your application's IServiceProvider.
public static class Program
{
    public static void Main()
    {
        // Build the service provider.
        var appBuilder = ConsoleApp.CreateBuilder();
        appBuilder.Services.AddSingleton<IMessageWriter, ConsoleMessageWriter>();
        appBuilder.Services.AddConsoleMain<MyService>();
        using var app = appBuilder.Build();

        // Configure the ServiceProviderProvider so that all [Dependency] fields
        // are resolved from this service provider.
        ServiceProviderProvider.ServiceProvider = () => app.Services;

        app.Run();
    }
}
