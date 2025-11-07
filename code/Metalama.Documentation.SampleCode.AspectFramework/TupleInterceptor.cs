// This is public domain Metalama sample code.

using System;

namespace Doc.TupleInterceptor;

public class Invoice
{
    [Intercept]
    public void AddProduct( string productCode, decimal quantity )
    {
        Console.WriteLine( $"Adding {quantity}x {productCode}." );
    }
    
    [Intercept]
    public void Cancel()
    {
        Console.WriteLine( $"Cancelling the order." );
    }
}

public static class Interceptor
{
    // Interceptor method expecting a single payload parameter.
    public static void Intercept<T>( Action<T> action, in T args )
    {
        Console.WriteLine( "Intercepted!" );
        action( args );
    }
}