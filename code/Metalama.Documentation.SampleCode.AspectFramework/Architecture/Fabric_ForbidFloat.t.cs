// Warning LAMA0905 on `double`: `The 'double' type cannot be referenced by the 'Doc.Architecture.Fabric_ForbidFloat.Invoicing' namespace. Use decimal numbers instead.`
using Metalama.Extensions.Architecture;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;
namespace Doc.Architecture.Fabric_ForbidFloat
{
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  internal class Fabric : ProjectFabric
  {
    public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  namespace Invoicing
  {
    internal class Invoice
    {
      public double Amount { get; set; }
    }
  }
}