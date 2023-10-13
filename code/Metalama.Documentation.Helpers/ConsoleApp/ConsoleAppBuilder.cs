﻿// This is public domain Metalama sample code.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Metalama.Documentation.Helpers.ConsoleApp;

public class ConsoleAppBuilder
{
    private List<Action<ILoggingBuilder>> _configureLoggingActions = new();
    private readonly ServiceCollection _serviceCollection = new();

    public IServiceCollection Services => this._serviceCollection;

    public ConsoleAppBuilder ConfigureLogging( Action<ILoggingBuilder> build )
    {
        this._configureLoggingActions.Add( build );

        return this;
    }

    public ConsoleApp Build( IReadOnlyList<string>? arguments = null )
    {
        this._serviceCollection.AddLogging(
            logging =>
            {
                logging.AddConsole();

                foreach ( var action in this._configureLoggingActions )
                {
                    action( logging );
                }
            } );

        this._serviceCollection.AddSingleton<IConsoleHost>( _ => new ConsoleHost( arguments ) );

        return new ConsoleApp( this._serviceCollection.BuildServiceProvider() );
    }
}