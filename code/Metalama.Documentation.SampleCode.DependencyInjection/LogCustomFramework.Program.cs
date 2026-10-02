// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;

namespace Doc.LogCustomFramework;

// Program entry point. Creates the host, configure dependencies, and runs it.
public static class Program
{
    private static void Main()
    {
        var builder = ConsoleApp.CreateBuilder();
        builder.Services.AddConsoleMain<ConsoleMain>();
        using var app = builder.Build();
        app.Run();
    }
}