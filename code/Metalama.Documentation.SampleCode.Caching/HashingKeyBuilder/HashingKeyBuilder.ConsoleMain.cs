// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;

namespace Doc.HashKeyBuilder;

public sealed class ConsoleMain( FileSystem fileSystem ) : IConsoleMain
{
    public void Execute()
    {
        for ( var i = 0; i < 3; i++ )
        {
            var value = fileSystem.ReadAll( Environment.ProcessPath! );
            Console.WriteLine( $"FileSystem returned {value.Length} bytes." );
        }

        Console.WriteLine(
            $"In total, FileSystem performed {fileSystem.OperationCount} operation(s)." );
    }
}