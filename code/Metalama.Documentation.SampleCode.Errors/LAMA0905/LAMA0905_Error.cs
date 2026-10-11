// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;
using System;

namespace Doc.LAMA0905.Error;

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
        // OrderController is not allowed to use the Order class.
        Console.WriteLine( new Order() );
    }
}
