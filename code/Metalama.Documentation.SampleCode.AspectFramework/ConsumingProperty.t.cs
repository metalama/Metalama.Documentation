using Metalama.Framework.Aspects;
using System;
namespace Doc.ConsumingProperty;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class Log : OverrideMethodAspect
{
  public string? Category { get; set; }
  public override dynamic? OverrideMethod() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}