// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;

namespace Doc.LAMA0210.Fixed;

[RunTimeOrCompileTime]
public record RetryPolicy( int MaxAttempts ) : ICompileTimeSerializable
{
    // The record now provides its own serializer.
    public class Serializer : ReferenceTypeSerializer<RetryPolicy>
    {
        public override RetryPolicy CreateInstance( IArgumentsReader constructorArguments )
            => new( constructorArguments.GetValue<int>( "MaxAttempts" ) );

        public override void SerializeObject( RetryPolicy obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments )
            => constructorArguments.SetValue( "MaxAttempts", obj.MaxAttempts );
    }
}

public class OrderService
{
    [Retry( 3 )]
    public void PlaceOrder( int orderId )
    {
        Console.WriteLine( $"Placing order {orderId}." );
    }
}
