// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1000.Error;

public static class LegacyLogger
{
    public static void Write( string message ) => Console.WriteLine( message );
}

public static class Logger
{
    public static void Write( string message ) => Console.WriteLine( $"[{DateTime.Now:T}] {message}" );
}

[RedirectLogging]
internal class Checkout
{
    public void Complete() => LegacyLogger.Write( "Checkout completed." );
}

internal class OrderService
{
    public void Place() => LegacyLogger.Write( "Order placed." );
}
