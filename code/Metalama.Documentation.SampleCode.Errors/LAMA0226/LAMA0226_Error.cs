// This is public domain Metalama sample code.

using System;
using System.Collections.Generic;

namespace Doc.LAMA0226.Error;

public class AuditEntry
{
    public AuditEntry( object? value )
    {
        this.Value = value;
    }

    public object? Value { get; }
}

public static class AuditLog
{
    public static void Write( string methodName, Dictionary<string, AuditEntry> entries )
        => Console.WriteLine( $"{methodName} called with {entries.Count} argument(s)." );
}

internal class OrderService
{
    [Audit]
    public void PlaceOrder( string product, int quantity ) { }
}
