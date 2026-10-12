// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0221.Fixed;

internal class OrderService
{
    [Audit]
    public void PlaceOrder( string product ) { }
}

internal static class AuditLog
{
    public static void Record( object instance, string methodName )
        => Console.WriteLine( $"{methodName} called on {instance}." );
}
