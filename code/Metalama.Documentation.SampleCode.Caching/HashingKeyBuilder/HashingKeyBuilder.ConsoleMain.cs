// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Helpers.ConsoleApp;
using Microsoft.Extensions.Hosting;
using System;

namespace Doc.HashKeyBuilder
{
    public sealed class ConsoleMain : IConsoleMain
    {
        private readonly FileSystem _fileSystem;
        
        public ConsoleMain( FileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
        }

        public void Execute()
        {
            for ( var i = 0; i < 3; i++ )
            {
                var value = this._fileSystem.ReadAll( Environment.ProcessPath! );
                Console.WriteLine( $"FileSystem returned {value.Length} bytes." );
            }

            Console.WriteLine( $"In total, FileSystem performed {this._fileSystem.OperationCount} operation(s)." );
        }
    }
}