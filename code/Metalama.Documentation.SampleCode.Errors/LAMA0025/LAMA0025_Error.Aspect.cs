// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0025.Error;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Error: the aspect has no template named 'LogTemplate'.
        builder.Override( "LogTemplate" );
    }

    [Template]
    private dynamic? OverrideTemplate()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
