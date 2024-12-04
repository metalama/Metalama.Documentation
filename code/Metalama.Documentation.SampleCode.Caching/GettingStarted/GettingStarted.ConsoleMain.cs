// This is public domain Metalama sample code.

using System;
using Metalama.Documentation.Helpers.ConsoleApp;

namespace Doc.GettingStarted;

public sealed class ConsoleMain : IConsoleMain
{
    private readonly CloudCalculator _cloudCalculator;

    public ConsoleMain( CloudCalculator cloudCalculator )
    {
        this._cloudCalculator = cloudCalculator;
    }

    public void Execute()
    {
        for ( var i = 0; i < 3; i++ )
        {
            var value = this._cloudCalculator.Add( 1, 1 );
            Console.WriteLine( $"CloudCalculator returned {value}." );
        }

        Console.WriteLine(
            $"In total, CloudCalculator performed {this._cloudCalculator.OperationCount} operation(s)." );
    }
}