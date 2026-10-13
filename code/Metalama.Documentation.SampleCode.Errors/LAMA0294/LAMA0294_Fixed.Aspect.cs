// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0294.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        // Fixed: compile-time code uses a regular array instead of an inline array.
        var indexes = new int[4];

        for ( var i = 0; i < 4; i++ )
        {
            indexes[i] = i;
        }
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
