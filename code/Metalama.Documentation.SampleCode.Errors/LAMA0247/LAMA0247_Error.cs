// This is public domain Metalama sample code.

using System;
using System.IO;

namespace Doc.LAMA0247.Error;

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

    // Error: the method returns void, so the aspect cannot return its result from a bool local function.
    [RetryOnFailure]
    public void Clear() => Console.WriteLine( "Clearing the store." );
}
