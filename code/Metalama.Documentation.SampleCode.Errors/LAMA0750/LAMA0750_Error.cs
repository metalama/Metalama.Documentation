// This is public domain Metalama sample code.

namespace Doc.LAMA0750.Error;

// Error: the aspect introduces a method, but an enum can't receive introduced members.
[TypeName]
public enum OrderStatus
{
    Pending,
    Shipped,
    Delivered
}

public class Order
{
    public OrderStatus Status { get; set; }
}
