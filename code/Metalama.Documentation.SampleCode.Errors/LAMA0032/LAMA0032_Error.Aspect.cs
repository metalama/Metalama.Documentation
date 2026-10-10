// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0032.Error;

public class LogAttribute : MethodAspect
{
    public string? Category { get; set; }

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        if ( this.Category == null )
        {
            builder.Override( "Log" );
        }
        else
        {
            builder.Override( "Log", args: new { category = this.Category } );
        }
    }

    [Template]
    private dynamic? Log()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }

    // Error: two templates cannot have the same name, even with different parameters.
    [Template]
    private dynamic? Log( [CompileTime] string category )
    {
        Console.WriteLine( $"{category}: executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
