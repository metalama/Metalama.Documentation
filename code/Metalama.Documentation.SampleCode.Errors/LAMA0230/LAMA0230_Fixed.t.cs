using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
namespace Doc.LAMA0230.Fixed;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class OrderService
{
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Executing OrderService.PlaceOrder(string, int).");
  }
  public void CancelOrder(int orderId)
  {
    Console.WriteLine("Executing OrderService.CancelOrder(int).");
  }
  // Fixed: the type fabric is private.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}