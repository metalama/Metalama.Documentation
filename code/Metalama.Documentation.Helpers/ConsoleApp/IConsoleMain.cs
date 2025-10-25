// This is public domain Metalama sample code.

namespace Metalama.Documentation.Helpers.ConsoleApp;

public interface IConsoleMain
{
    void Execute();
}

public interface IConsoleHost
{
    IReadOnlyList<string> Arguments { get; }
}

internal class ConsoleHost : IConsoleHost
{
    public ConsoleHost( IReadOnlyList<string>? arguments )
    {
        this.Arguments = arguments ?? Array.Empty<string>();
    }

    public IReadOnlyList<string> Arguments { get; }
}