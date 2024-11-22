// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using System.Threading.Tasks;

namespace Doc.Redis;

public sealed class ConsoleMain( CloudCalculator cloudCalculator ) : IAsyncConsoleMain
{
    public Task ExecuteAsync()
    {
        for ( var i = 0; i < 3; i++ )
        {
            var value = cloudCalculator.Add( 1, 1 );
            Console.WriteLine( $"CloudCalculator returned {value}." );
        }

        Console.WriteLine(
            $"In total, CloudCalculator performed {cloudCalculator.OperationCount} operation(s)." );

        return Task.CompletedTask;
    }
}