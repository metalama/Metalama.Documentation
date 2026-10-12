using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
namespace Doc.LAMA0260.Fixed;
// Fixed: the class no longer has the [RunTimeOrCompileTime] attribute, so it's a run-time type.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class PriceCalculator
{
  public decimal ApplyDiscount(decimal price, decimal rate)
  {
    Console.WriteLine("Executing PriceCalculator.ApplyDiscount(decimal, decimal).");
    return price * (1 - rate);
  }
  public decimal AddTax(decimal price, decimal rate)
  {
    Console.WriteLine("Executing PriceCalculator.AddTax(decimal, decimal).");
    return price * (1 + rate);
  }
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}