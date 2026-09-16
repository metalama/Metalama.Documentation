// This is public domain Metalama sample code.

using Microsoft.Extensions.DependencyInjection;

namespace Metalama.Documentation.Helpers.ConsoleApp;

public static class ConsoleAppFactory
{
    public static IServiceCollection AddConsoleMain<T>( this IServiceCollection collection )
        where T : class, IConsoleMain
        => collection.AddSingleton<IConsoleMain, T>();

    public static IServiceCollection AddConsoleMain(
        this IServiceCollection collection,
        Func<IServiceProvider, IConsoleMain> factory )
        => collection.AddSingleton( factory );

    public static IServiceCollection AddAsyncConsoleMain<T>( this IServiceCollection collection )
        where T : class, IAsyncConsoleMain
        => collection.AddSingleton<IAsyncConsoleMain, T>();
}