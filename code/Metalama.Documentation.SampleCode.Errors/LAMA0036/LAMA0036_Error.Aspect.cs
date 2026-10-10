// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0036.Error;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.LogTemplate) );
    }

    [Template]
    public dynamic? LogTemplate()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}

public class TimedLogAttribute : LogAttribute
{
    // Error: the base class already defines a template named LogTemplate.
    [Template]
    public new dynamic? LogTemplate()
    {
        Console.WriteLine( $"{DateTime.Now:T} Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
