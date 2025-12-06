using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;
namespace Doc.OverrideMethodAspect_;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[AttributeUsage(AttributeTargets.Method)]
public abstract class OverrideMethodAspect : Attribute, IAspect<IMethod>
{
  public virtual void BuildAspect(IAspectBuilder<IMethod> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  public virtual void BuildEligibility(IEligibilityBuilder<IMethod> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  [Template]
  public abstract dynamic? OverrideMethod();
}