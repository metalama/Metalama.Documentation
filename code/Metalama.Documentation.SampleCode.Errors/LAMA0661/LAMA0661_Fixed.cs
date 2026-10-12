// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0661.Fixed;

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

        // The lambda parameter has another name.
        Array.ForEach( items, item => LegacyLogger.Write( $"Item {item}." ) );
    }
}
