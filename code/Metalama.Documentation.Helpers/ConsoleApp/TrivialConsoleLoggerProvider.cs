// This is public domain Metalama sample code.

using Microsoft.Extensions.Logging;

namespace Metalama.Documentation.Helpers.ConsoleApp;

/// <summary>
/// A trivial implementation of <see cref="ILoggerProvider"/> that logs to <see cref="Console"/>.
/// Unlike the system ConsoleLoggerProvider, this class does not queue items and has deterministic behavior upon disposal.
/// </summary>
internal class TrivialConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger( string categoryName ) => new Logger();

    void IDisposable.Dispose() { }

    private class Logger : ILogger
    {
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter )
        {
            Console.WriteLine( formatter( state, exception ) );
        }

        public bool IsEnabled( LogLevel logLevel ) => true;

        public IDisposable? BeginScope<TState>( TState state ) where TState : notnull
            => throw new NotImplementedException();
    }
}