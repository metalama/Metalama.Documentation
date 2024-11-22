// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using System.IO;

namespace Doc.Formatter;

public sealed class ConsoleMain( FileSystem fileSystem ) : IConsoleMain
{
    public void Execute()
    {
        var fileInfo = new FileInfo( Environment.ProcessPath! );

        for ( var i = 0; i < 3; i++ )
        {
            var value = fileSystem.ReadAll( fileInfo );
            Console.WriteLine( $"FileSystem returned {value.Length} bytes." );
        }

        Console.WriteLine(
            $"In total, FileSystem performed {fileSystem.OperationCount} operation(s)." );
    }
}