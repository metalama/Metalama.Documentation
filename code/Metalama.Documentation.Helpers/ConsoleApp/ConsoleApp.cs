// This is public domain Metalama sample code.

using Microsoft.Extensions.DependencyInjection;

namespace Metalama.Documentation.Helpers.ConsoleApp;

public class ConsoleApp( ServiceProvider serviceProvider ) : IDisposable, IAsyncDisposable
{
    public ServiceProvider Services { get; } = serviceProvider;

    public static ConsoleAppBuilder CreateBuilder() => new();

    public void Run()
    {
        var service = this.Services.GetRequiredService<IConsoleMain>();

        service.Execute();
    }

    public async Task RunAsync()
    {
        var service = this.Services.GetRequiredService<IAsyncConsoleMain>();

        await service.ExecuteAsync();
    }

    public void Dispose()
    {
        this.Services.Dispose();
    }

    public ValueTask DisposeAsync() => this.Services.DisposeAsync();
}