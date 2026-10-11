// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0661.Error;

public static class LegacyLogger
{
    public static void Write( string message ) => Console.WriteLine( message );
}

public static class Logger
{
    public static void Write( string message, int requestId ) => Console.WriteLine( $"{message} (request {requestId})" );
}

[UseModernLogger]
public class OrderProcessor
{
    public void Process( int id, int[] items )
    {
        LegacyLogger.Write( "Processing started." );

        // The lambda parameter hides the parameter of the method.
        Array.ForEach( items, id => LegacyLogger.Write( $"Item {id}." ) );
    }
}
