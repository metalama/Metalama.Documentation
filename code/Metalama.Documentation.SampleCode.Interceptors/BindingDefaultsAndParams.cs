// This is public domain Metalama sample code.

using System;

namespace Doc.BindingDefaultsAndParams;

[RedirectLogging]
public class OrderProcessor
{
    public void Process( int orderId )
    {
        Log.Write( "Processing an order." );
        Log.Write( "Order {0} shipped to {1}.", Severity.Warning, orderId, "Paris" );
    }
}

public enum Severity
{
    Info,
    Warning
}

public static class Log
{
    public static void Write(
        string format,
        Severity severity = Severity.Info,
        params object[] args )
        => Console.WriteLine( $"{severity}: {string.Format( format, args )}" );
}
