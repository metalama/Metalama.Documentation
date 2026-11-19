// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0888.Fixed;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Fixed: the 'out' argument is a local, which is passed to the template as an argument.
        var prefix = builder.Project.TryGetProperty( "LogPrefix", out var value ) ? value : "Entering";

        builder.Override( nameof(this.LogTemplate), args: new { prefix } );
    }

    [Template]
    public dynamic? LogTemplate( [CompileTime] string prefix )
    {
        Console.WriteLine( $"{prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
