// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The program does not run until metalama/Metalama#2235 is fixed:
// [ExcludeInterceptors] has no effect.
// @DisableExecuteProgram
#endif

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
