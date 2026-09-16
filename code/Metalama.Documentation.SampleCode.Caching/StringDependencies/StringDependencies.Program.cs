// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.StringDependencies;

internal static class Program
{
    public static void Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // Add the caching service.
        builder.Services.AddMetalamaCaching();

        // Add other components as usual, then run the application.
        builder.Services.AddConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<ProductCatalogue>();

        var host = builder.Build();
        host.Run();
    }
}