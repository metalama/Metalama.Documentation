// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using Metalama.Framework.Utilities;
using System;

namespace Doc.LAMA0210.Error;

// Metalama cannot generate a serializer for a positional record.
[RunTimeOrCompileTime]
[ImmutableType]
public record RetryPolicy( int MaxAttempts ) : ICompileTimeSerializable;

public class OrderService
{
    [Retry( 3 )]
    public void PlaceOrder( int orderId )
    {
        Console.WriteLine( $"Placing order {orderId}." );
    }
}
