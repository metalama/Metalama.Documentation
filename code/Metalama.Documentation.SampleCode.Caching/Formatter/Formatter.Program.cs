// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Formatter;

internal static class Program
{
    public static void Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // [<snippet Registration>]
        // Add the caching service.
        builder.Services.AddMetalamaCaching( caching => caching.ConfigureFormatters( formatters
                                                 => formatters
                                                     .AddFormatter( r => new FileInfoFormatter(
                                                         r ) ) ) );

        // [<endsnippet Registration>]

        // Add other components as usual.
        builder.Services.AddConsoleMain<ConsoleMain>();

        builder.Services.AddSingleton<FileSystem>();

        // Run the main service.
        using var app = builder.Build();

        app.Run();
    }
}