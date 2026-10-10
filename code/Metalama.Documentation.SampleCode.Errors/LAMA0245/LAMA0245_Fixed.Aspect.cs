// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0245.Fixed;

public class PrintDefaultValueAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.PrintDefaultValue), args: new { T = typeof(int) } );
    }

    [Template]
    public void PrintDefaultValue<[CompileTime] T>()
    {
        // Fixed: default(T) is emitted as run-time code, where T is replaced by the actual type.
        Console.WriteLine( default(T) );
    }
}
