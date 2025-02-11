// Warning MY001 on `InvalidClass`: `The type 'InvalidClass' must have a public default constructor.`
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using System.Linq;
namespace Doc.Architecture.RequireDefaultConstructorFabric;
// Reusable implementation of the architecture rule.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[CompileTime]
internal static class ArchitectureExtensions
{
  private static readonly DiagnosticDefinition<INamedType> _warning = new("MY001", Severity.Warning, "The type '{0}' must have a public default constructor.");
  public static void MustHaveDefaultConstructor(this IQuery<INamedType> verifier) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
// This class has an implicit default constructor.
public class ValidClass1
{
}
// This class has an explicit default constructor.
public class ValidClass2
{
  public ValidClass2()
  {
  }
}
// This class does not havr any default constructor.
public class InvalidClass
{
  public InvalidClass(int x)
  {
  }
}