// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Documentation.Helpers.Security;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Backends.Azure;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Doc.AzureSynchronized;

internal static class Program
{
    public static async Task Main()
    {
        // We simulate two applications running in parallel.
        var app1 = RunApp( "App1" );
        var app2 = RunApp( "App2" );

        await Task.WhenAll( app1, app2 );
    }

    private static async Task RunApp( string name )
    {
        var builder = ConsoleApp.CreateBuilder();

        // Get the connection string.
        var connectionString = Secrets.Get( "CacheInvalidationTestServiceBusConnectionString" );

        // [<snippet AddMetalamaCaching>]
        // Add the caching service.
        builder.Services.AddMetalamaCaching( 
            caching =>
                caching.WithBackend(
                    backend =>
                        backend.Memory()
                            .WithAzureSynchronization(
                                connectionString ) ) );
        // [<endsnippet AddMetalamaCaching>]

        // Add other components as usual.
        builder.Services.AddAsyncConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<ProductCatalogue>();

        // Build the application.
        await using var app = builder.Build( [name] );

        // [<snippet Initialize>]
        await app.Services.GetRequiredService<ICachingService>().InitializeAsync();
        // [<endsnippet Initialize>]

        // Run the application.
        await app.RunAsync();
    }
}