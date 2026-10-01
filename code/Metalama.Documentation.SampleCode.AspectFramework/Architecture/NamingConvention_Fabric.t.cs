// Warning LAMA0906 on `TextLoader`: `The type 'TextLoader' does not respect the naming convention set by a fabric. The type name should match the "^.*Reader$" pattern.`
using Metalama.Extensions.Architecture;
using Metalama.Framework.Fabrics;
using System.IO;
namespace Doc.Architecture.NamingConvention_Fabric;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
// The naming convention is broken.
internal class TextLoader : TextReader
{
}