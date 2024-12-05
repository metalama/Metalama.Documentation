// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Doc.Logging;

internal static class Program
{
    public static void Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // [<snippet AddLogging>]
        // Add logging.
        builder.ConfigureLogging( 
            logging =>
                logging.SetMinimumLevel( LogLevel.Debug ) ); 
        // [<endsnippet AddLogging>]

        // Add the caching service.
        builder.Services.AddMetalamaCaching();

        // Add other components as usual, then run the application.
        builder.Services.AddConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<CloudCalculator>();

        using var app = builder.Build();
        app.Run();
    }
}