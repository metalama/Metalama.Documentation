// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.LAMA0278.Fixed;

// Fixed: the aspect is a class.
internal class LogAspect : IAspect<IMethod>
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
