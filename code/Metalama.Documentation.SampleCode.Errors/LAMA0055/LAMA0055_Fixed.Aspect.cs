// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.LAMA0055.Fixed;

[AttributeUsage( AttributeTargets.Class | AttributeTargets.Method )]
public class LogAttribute : OverrideMethodAspect, IAspect<INamedType>
{
    public void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Adds the aspect to all methods of the type. The methods are processed after the type, so this is allowed.
        builder.Outbound.SelectMany( t => t.Methods ).AddAspectIfEligible( _ => this );
    }

    public void BuildEligibility( IEligibilityBuilder<INamedType> builder ) { }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
