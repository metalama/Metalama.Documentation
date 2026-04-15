// This is public domain Metalama sample code.

using System;

namespace Doc.AfterLastInstanceConstructor;

[PublishWhenCreated]
public partial class Order
{
    public string CustomerId { get; }

    public Order( string customerId )
    {
        this.CustomerId = customerId;
    }
}

public partial class RecurringOrder : Order
{
    public TimeSpan Interval { get; }

    public RecurringOrder( string customerId, TimeSpan interval )
        : base( customerId )
    {
        this.Interval = interval;
    }
}
