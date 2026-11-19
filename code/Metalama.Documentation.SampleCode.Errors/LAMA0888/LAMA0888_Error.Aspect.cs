// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0888.Error;

public class LogAttribute : MethodAspect
{
    private string? _prefix;

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Warning: the aspect field is passed as an 'out' argument outside a constructor.
        builder.Project.TryGetProperty( "LogPrefix", out this._prefix );

        builder.Override( nameof(this.LogTemplate) );
    }

    [Template]
    public dynamic? LogTemplate()
    {
        var prefix = this._prefix ?? "Entering";
        Console.WriteLine( $"{prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
