using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
namespace Doc.LAMA0038.Fixed;
internal class ServiceBase
{
  // Fixed: the aspect is added directly to the method of the base class.
  [Log]
  public void Ping()
  {
    Console.WriteLine("Executing ServiceBase.Ping().");
  }
}
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class OrderService : ServiceBase
{
  public void PlaceOrder(int productId)
  {
    Console.WriteLine("Executing OrderService.PlaceOrder(int).");
  }
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}