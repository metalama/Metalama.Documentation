// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0032.Fixed;

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
            builder.Override( "LogWithCategory", args: new { category = this.Category } );
        }
    }

    [Template]
    private dynamic? Log()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }

    // Fixed: each template has a unique name.
    [Template]
    private dynamic? LogWithCategory( [CompileTime] string category )
    {
        Console.WriteLine( $"{category}: executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
