// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;
using System.IO;

namespace Doc.Formatter;

public sealed class ConsoleMain : IConsoleMain
{
    private readonly FileSystem _fileSystem;

    public ConsoleMain( FileSystem fileSystem )
    {
        this._fileSystem = fileSystem;
    }

    public void Execute()
    {
        var fileInfo = new FileInfo( Environment.ProcessPath! );

        for ( var i = 0; i < 3; i++ )
        {
            var value = this._fileSystem.ReadAll( fileInfo );
            Console.WriteLine( $"FileSystem returned {value.Length} bytes." );
        }

        Console.WriteLine(
            $"In total, FileSystem performed {this._fileSystem.OperationCount} operation(s)." );
    }
}