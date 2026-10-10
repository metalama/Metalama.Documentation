// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0289.Fixed;

internal class SessionService
{
    [LogToken]
    public void Open( string token ) => Console.WriteLine( "Session opened." );
}
