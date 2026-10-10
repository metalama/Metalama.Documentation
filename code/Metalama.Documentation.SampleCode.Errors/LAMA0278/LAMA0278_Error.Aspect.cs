// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.LAMA0278.Error;

// Error: an aspect must be a class, not a struct.
internal struct LogAspect : IAspect<IMethod>
{
    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.OverrideMethod) );
    }

    public void BuildEligibility( IEligibilityBuilder<IMethod> builder ) { }

    [Template]
    private dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
