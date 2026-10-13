// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0036.Fixed;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.LogTemplate) );
    }

    [Template]
    public virtual dynamic? LogTemplate()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}

public class TimedLogAttribute : LogAttribute
{
    // Fixed: the template overrides the virtual template of the base class.
    public override dynamic? LogTemplate()
    {
        Console.WriteLine( $"{DateTime.Now:T} Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
