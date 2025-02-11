    // Warning LAMA0905 on `TurnOn`: `The 'CofeeMachine' type cannot be referenced by the 'Bar.OrderCoffee()' method.`
using Metalama.Extensions.Architecture;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
namespace Doc.Architecture.Fabric_CustomPredicate;
// This class is the actual implementation of the predicate.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class MethodNamePredicate : ReferenceEndPredicate
{
  private readonly string _suffix;
  public MethodNamePredicate(ReferencePredicateBuilder builder, string suffix) : base(builder)
  {
    this._suffix = suffix;
  }
  protected override ReferenceGranularity GetGranularity() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  public override bool IsMatch(ReferenceEnd referenceEnd) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
// This class exposes the predicate as an extension method. It is your public API.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[CompileTime]
public static class Extensions
{
  public static ReferencePredicate MethodNameEndsWith(this ReferencePredicateBuilder builder, string suffix) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
// Here is how your new predicate can be used.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
// This is the class whose access are validated.
internal static class CofeeMachine
{
  public static void TurnOn()
  {
  }
}
internal class Bar
{
  public static void OrderCoffee()
  {
    // Forbidden because the method name does not end with Politely.
    CofeeMachine.TurnOn();
  }
  public static void OrderCoffeePolitely()
  {
    // Allowed.
    CofeeMachine.TurnOn();
  }
}