// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Doc.LogDefaultFramework;

// Program entry point. Creates the host, configure dependencies, and runs it.
public static class Program
{
    private static void Main()
    {
        var appBuilder = ConsoleApp.CreateBuilder();
        appBuilder.Services.AddSingleton<IMessageWriter, MessageWriter>();
        appBuilder.Services.AddConsoleMain<Worker>();
        using var app = appBuilder.Build();
        app.Run();
    }
}

// Definition of the interface consumed by the aspect.
public interface IMessageWriter
{
    void Write( string message );
}

// Implementation actually consumed by the aspect.
public class MessageWriter : IMessageWriter
{
    public void Write( string message )
    {
        Console.WriteLine( message );
    }
}