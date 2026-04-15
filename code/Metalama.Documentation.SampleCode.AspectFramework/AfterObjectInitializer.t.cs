using System;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.AfterObjectInitializer;
[ValidateAfterInitialization]
public partial class Invoice : IInitializable
{
  public required string Number { get; init; }
  public required decimal Amount { get; init; }
  public virtual void Initialize(InitializationContext context = default)
  {
    Console.WriteLine("Validating Invoice.");
  }
}
public partial class CreditNote : Invoice
{
  public required string Reason { get; init; }
  public override void Initialize(InitializationContext context = default)
  {
    base.Initialize(context);
    Console.WriteLine("Validating CreditNote.");
  }
}