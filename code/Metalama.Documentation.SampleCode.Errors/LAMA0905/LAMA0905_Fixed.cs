// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;
using System;

namespace Doc.LAMA0905.Fixed;

[CanOnlyBeUsedFrom( Types = [typeof( OrderFactory )], Description = "Use OrderFactory to create orders." )]
public class Order
{
    public decimal Amount { get; set; }
}

public class OrderFactory
{
    public Order Create( decimal amount ) => new Order { Amount = amount };
}

public class OrderController
{
    public void Submit( decimal amount )
    {
        // OrderController uses the factory instead of the Order class.
        Console.WriteLine( new OrderFactory().Create( amount ) );
    }
}
