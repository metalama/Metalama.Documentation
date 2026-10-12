// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Numerics;

namespace Doc.LAMA0265.Fixed;

public class AddPrintZeroAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.PrintZero), args: new { T = typeof(int) } );
    }

    [Template]
    public void PrintZero<[CompileTime] T>()
        where T : INumber<T>
    {
        // Fixed: the static interface member is accessed by a run-time generic helper method.
        Console.WriteLine( NumberHelper.Zero<T>() );
    }
}

public static class NumberHelper
{
    public static T Zero<T>()
        where T : INumber<T>
        => T.Zero;
}
