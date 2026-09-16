using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects.Configuration;
using System;
namespace Doc.AbsoluteExpiration_Fabric;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}