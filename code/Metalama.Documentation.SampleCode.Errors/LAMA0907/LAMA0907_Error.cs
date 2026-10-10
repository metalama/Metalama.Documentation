// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0907.Error;

[CanOnlyBeUsedFrom( Types = [typeof( OrderFactory )], ExclusionPredicateType = typeof( ExcludeTestTypesPredicate ) )]
public class Order
{
    public decimal Amount { get; set; }
}

public class OrderFactory
{
    public Order Create( decimal amount ) => new Order { Amount = amount };
}

public class OrderTests
{
    public void CanCreateOrder()
    {
        var order = new Order();
        _ = order;
    }
}
