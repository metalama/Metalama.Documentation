using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using System.Linq;
namespace Doc.AdvisingTypeFabricDiagnostics;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public partial class MyClass
{
  public string? Name { get; set; }
  public override string ToString()
  {
    return "MyClass (advised by fabric)";
  }
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    private static readonly DiagnosticDefinition<INamedType> _warning = new("MY001", Severity.Warning, "The type '{0}' should have a 'Name' property.");
    [Template]
    public string ToStringTemplate() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}
