using System;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA0044.Fixed;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class OrderService
{
  public void PlaceOrder([NotNull] string customerId)
  {
    if (customerId == null !)
    {
      throw new ArgumentNullException("customerId", "The 'customerId' parameter must not be null.");
    }
  }
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}