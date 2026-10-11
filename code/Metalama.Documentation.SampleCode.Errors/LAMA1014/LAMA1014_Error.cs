// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1014.Error;

internal static class Logger
{
    public static void Write( object message ) => Console.WriteLine( message );
}

[InterceptLogging]
internal static class Checkout
{
    public static void Run() => Logger.Write( 42 );
}
