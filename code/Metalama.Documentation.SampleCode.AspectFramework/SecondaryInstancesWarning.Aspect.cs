// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System;

namespace Doc.SecondaryInstancesWarning;

public class LogAttribute : OverrideMethodAspect
{
    private static readonly DiagnosticDefinition<int> _duplicateWarning = new(
        "MY001",
        Severity.Warning,
        "The [Log] aspect is applied {0} times to the same method. Only the primary instance will be used." );

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        if ( builder.AspectInstance.SecondaryInstances.Length > 0 )
        {
            builder.Diagnostics.Report(
                _duplicateWarning.WithArguments(
                    builder.AspectInstance.SecondaryInstances.Length + 1 ) );
        }

        base.BuildAspect( builder );
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method.DeclaringType.Name}.{meta.Target.Method.Name}" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Leaving {meta.Target.Method.DeclaringType.Name}.{meta.Target.Method.Name}" );
        }
    }
}
