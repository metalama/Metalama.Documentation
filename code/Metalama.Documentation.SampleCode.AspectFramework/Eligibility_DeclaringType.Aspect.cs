// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.Eligibility_DeclaringType;

internal class StaticLogAttribute : OverrideMethodAspect
{
    public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
    {
        base.BuildEligibility( builder );

        // The aspect must only be used on static classes.
        builder.DeclaringType().MustBeStatic();
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}" );

        return meta.Proceed();
    }
}