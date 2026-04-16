using Metalama.Framework.RunTime.Initialization;
namespace Doc.InitializerSlot;
[Validate]
[Publish]
public partial class Order : IInitializable
{
  public string OrderId { get; init; } = "";
  public string CustomerId { get; init; } = "";
  public virtual void Initialize(InitializationContext context = default)
  {
    if (!context.IsHandled(InitializerSlots.Validate))
    {
      ValidationService.Validate(this);
    }
    if (!context.IsHandled(InitializerSlots.Publish))
    {
      PublishService.Publish(this);
    }
  }
}
public partial class SubscriptionOrder : Order
{
  public int RenewalIntervalDays { get; init; }
  public override void Initialize(InitializationContext context = default)
  {
    base.Initialize(context.Descend(InitializerSlots.Validate | InitializerSlots.Publish));
    if (!context.IsHandled(InitializerSlots.Validate))
    {
      ValidationService.Validate(this);
    }
    if (!context.IsHandled(InitializerSlots.Publish))
    {
      PublishService.Publish(this);
    }
  }
}