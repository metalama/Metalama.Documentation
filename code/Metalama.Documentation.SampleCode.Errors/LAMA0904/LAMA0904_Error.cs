// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0904.Error;

[CanOnlyBeUsedFrom( Description = "Use OrderFactory to create orders." )] // No scope property is set.
public class Order
{
    public decimal Amount { get; set; }
}

public class OrderFactory
{
    public Order Create( decimal amount ) => new Order { Amount = amount };
}
