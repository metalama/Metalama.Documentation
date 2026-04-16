// This is public domain Metalama sample code.

namespace Doc.InitializerSlot;

[Validate]
[Publish]
public partial class Order
{
    public string OrderId { get; init; } = "";

    public string CustomerId { get; init; } = "";
}

public partial class SubscriptionOrder : Order
{
    public int RenewalIntervalDays { get; init; }
}
