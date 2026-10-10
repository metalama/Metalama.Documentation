// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0904.Fixed;

[CanOnlyBeUsedFrom( Types = [typeof( OrderFactory )], Description = "Use OrderFactory to create orders." )] // The Types property defines the allowed scope.
public class Order
{
    public decimal Amount { get; set; }
}

public class OrderFactory
{
    public Order Create( decimal amount ) => new Order { Amount = amount };
}
