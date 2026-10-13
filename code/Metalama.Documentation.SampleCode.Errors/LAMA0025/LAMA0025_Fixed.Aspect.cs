// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0025.Fixed;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Fixed: nameof guarantees that the template name matches an existing member.
        builder.Override( nameof(OverrideTemplate) );
    }

    [Template]
    private dynamic? OverrideTemplate()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
