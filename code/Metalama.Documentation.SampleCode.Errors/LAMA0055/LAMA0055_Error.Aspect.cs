// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.LAMA0055.Error;

[AttributeUsage( AttributeTargets.Class | AttributeTargets.Method )]
public class LogAttribute : OverrideMethodAspect, IAspect<INamedType>
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        // Error: a method-level aspect cannot add an aspect of the same type to the declaring type,
        // because the type has already been processed.
        builder.Outbound.Select( m => m.DeclaringType ).AddAspect( _ => this );
    }

    public void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Adds the aspect to all methods of the type.
        builder.Outbound.SelectMany( t => t.Methods ).AddAspectIfEligible( _ => this );
    }

    public void BuildEligibility( IEligibilityBuilder<INamedType> builder ) { }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
