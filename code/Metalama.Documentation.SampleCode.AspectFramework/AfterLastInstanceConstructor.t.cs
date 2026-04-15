using System;
using Metalama.Framework.RunTime;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.AfterLastInstanceConstructor;
[PublishWhenCreated]
public partial class Order
{
  public string CustomerId { get; }
  public Order(string customerId, [AspectGenerated] InitializationContext context = default)
  {
    this.CustomerId = customerId;
    if (!context.IsHandled(InitializationSlot.OnConstructed))
    {
      this.OnConstructed(context);
    }
  }
  protected virtual void OnConstructed(InitializationContext context = default)
  {
    DomainEvents.Publish(new EntityCreated("Order", this));
  }
}
public partial class RecurringOrder : Order
{
  public TimeSpan Interval { get; }
  public RecurringOrder(string customerId, TimeSpan interval, [AspectGenerated] InitializationContext context = default) : base(customerId, context)
  {
    this.Interval = interval;
  }
}