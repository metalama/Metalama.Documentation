// This is public domain Metalama sample code.

using System;
using Metalama.Documentation.Helpers.ConsoleApp;

namespace Doc.Logging;

public sealed class ConsoleMain( CloudCalculator cloudCalculator ) : IConsoleMain
{
    public void Execute()
    {
        for ( var i = 0; i < 3; i++ )
        {
            var value = cloudCalculator.Add( 1, 1 );
            Console.WriteLine( $"CloudCalculator returned {value}." );
        }

        Console.WriteLine(
            $"In total, CloudCalculator performed {cloudCalculator.OperationCount} operation(s)." );
    }
}