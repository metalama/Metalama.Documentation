using System;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.AfterObjectInitializer;
[PublishWhenInitialized]
public partial record Document : IInitializable
{
  public required string Id { get; init; }
  public string Title { get; init; } = "Untitled";
  public virtual void Initialize(InitializationContext context = default)
  {
    DomainEvents.Publish(new EntityInitialized("Document", this));
  }
}
public partial record Report : Document
{
  public required DateOnly Date { get; init; }
}