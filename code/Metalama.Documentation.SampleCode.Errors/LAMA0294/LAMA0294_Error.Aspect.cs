// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Runtime.CompilerServices;

namespace Doc.LAMA0294.Error;

// Error: compile-time code can't declare an inline array.
[CompileTime]
[InlineArray( 4 )]
public struct ParameterIndexes
{
    private int _element0;
}

public class LogAttribute : OverrideMethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        var indexes = new ParameterIndexes();

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
