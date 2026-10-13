// This is public domain Metalama sample code.

using System;

namespace Doc.BindingWithArgsAnonymous;

[AddMetricContext]
public class OrderService
{
    public void PlaceOrder() => Metrics.Increment( "orders.placed" );
}

public static class Metrics
{
    public static void Increment( string name ) => Console.WriteLine( $"{name} +1" );
}

internal class Program
{
    private static void Main() => new OrderService().PlaceOrder();
}
