// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0068.Fixed;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Fixed: call the Override method.
        builder.Override( nameof(this.OverrideMethod) );
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
