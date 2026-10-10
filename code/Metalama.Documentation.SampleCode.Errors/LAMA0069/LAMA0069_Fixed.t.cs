// Warning DOC001 on `Ping`: `The method 'ServiceBase.Ping()' is a legacy method and should not be used in new code.`
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
namespace Doc.LAMA0069.Fixed;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class ServiceBase
{
  public void Ping()
  {
  }
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    private static readonly DiagnosticDefinition<IMethod> _legacyMethod = new("DOC001", Severity.Warning, "The method '{0}' is a legacy method and should not be used in new code.");
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class OrderService : ServiceBase
{
  public void PlaceOrder(int productId)
  {
  }
}