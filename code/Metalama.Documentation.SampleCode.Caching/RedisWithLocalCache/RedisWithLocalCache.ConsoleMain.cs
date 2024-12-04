// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using System.Threading.Tasks;

namespace Doc.RedisWithLocalCache;

public sealed class ConsoleMain : IAsyncConsoleMain
{
    private readonly CloudCalculator _cloudCalculator;

    public ConsoleMain( CloudCalculator cloudCalculator )
    {
        this._cloudCalculator = cloudCalculator;
    }

    public Task ExecuteAsync()
    {
        for ( var i = 0; i < 3; i++ )
        {
            var value = this._cloudCalculator.Add( 1, 1 );
            Console.WriteLine( $"CloudCalculator returned {value}." );
        }

        Console.WriteLine(
            $"In total, CloudCalculator performed {this._cloudCalculator.OperationCount} operation(s)." );

        return Task.CompletedTask;
    }
}