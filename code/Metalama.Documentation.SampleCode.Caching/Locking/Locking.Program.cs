// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Building;
using Metalama.Patterns.Caching.Locking;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Locking;

internal static class Program
{
    public static void Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // [<snippet AddMetalamaCaching>]
        // Add the caching service.
        builder.Services.AddMetalamaCaching( caching =>
                                                 caching.AddProfile(
                                                     new CachingProfile( "Locking" )
                                                     {
                                                         LockingStrategy =
                                                             new LocalLockingStrategy()
                                                     } ) );

        // [<endsnippet AddMetalamaCaching>]

        // Add other components as usual, then run the application.
        builder.Services.AddConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<CloudService>();

        using var app = builder.Build();
        app.Run();
    }
}