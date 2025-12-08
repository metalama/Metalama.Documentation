// This is public domain Metalama sample code.

using System;

namespace Doc.FactoryFramework;

// Base interface for services that are created by factories.
public interface IFactoredService { }

// Factory interface that creates instances of factored services.
public interface IServiceFactory<out T>
    where T : IFactoredService
{
    T Create();
}

// Example factored service interface.
public interface ILogger : IFactoredService
{
    void Log( string message );
}

// Example factory implementation (would be registered in DI container).
public class LoggerFactory : IServiceFactory<ILogger>
{
    public ILogger Create() => new ConsoleLogger();
}

// Example logger implementation.
public class ConsoleLogger : ILogger
{
    public void Log( string message ) => Console.WriteLine( message );
}
