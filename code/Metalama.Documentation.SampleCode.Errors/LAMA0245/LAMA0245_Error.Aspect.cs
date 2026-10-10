// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0245.Error;

public class PrintDefaultValueAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.PrintDefaultValue), args: new { T = typeof(int) } );
    }

    [Template]
    public void PrintDefaultValue<[CompileTime] T>()
    {
        // Error: T stands for a run-time type, so default(T) cannot be evaluated by meta.CompileTime.
        Console.WriteLine( meta.CompileTime( default(T) ) );
    }
}
