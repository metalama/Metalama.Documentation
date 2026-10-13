// This is public domain Metalama sample code.

using System;

namespace Doc.PassCallerInstance;

public class OrderService
{
    public void PlaceOrder( string product ) => Telemetry.Track( $"Order placed: {product}" );

    public static void Reset() => Telemetry.Track( "Orders reset" );
}

public static class Telemetry
{
    public static void Track( string eventName ) => Console.WriteLine( eventName );
}
