using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.OverrideMethodAspect_
{

#pragma warning disable CS0067, CS8618, CA1822, CS0162, CS0169, CS0414
    // <aspect>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class OverrideMethodAspect : Attribute, IAspect<IMethod>
    {
        public virtual void BuildAspect(IAspectBuilder<IMethod> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");


        public virtual void BuildEligibility(IEligibilityBuilder<IMethod> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");


        [Template]
        public abstract dynamic? OverrideMethod();
    }

#pragma warning restore CS0067, CS8618, CA1822, CS0162, CS0169, CS0414

    // </aspect>
}