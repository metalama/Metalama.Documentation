using Metalama.Framework.Aspects;
using System;
namespace Doc.LAMA0229.Fixed;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Executing OrderService.PlaceOrder(string, int).");
  }
}
// Fixed: the aspect class is declared at the namespace level.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class LogAttribute : OverrideMethodAspect
{
  public override dynamic? OverrideMethod() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}