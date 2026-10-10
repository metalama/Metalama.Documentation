// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0041.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public string? Category { get; set; }

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // The aspect throws an exception when the Category property isn't set.
        if ( this.Category == null )
        {
            throw new InvalidOperationException( "The Category property of the [Log] aspect must be set." );
        }

        base.BuildAspect( builder );
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{this.Category}: executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
