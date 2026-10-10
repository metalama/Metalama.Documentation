// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.LAMA0247.Fixed;

internal class FileStore
{
    [RetryOnFailure]
    public bool TryDelete( string path )
    {
        if ( !File.Exists( path ) )
        {
            return false;
        }

        File.Delete( path );

        return true;
    }

    // Fixed: the aspect is applied only to methods that return bool.
    public void Clear() => Console.WriteLine( "Clearing the store." );
}
