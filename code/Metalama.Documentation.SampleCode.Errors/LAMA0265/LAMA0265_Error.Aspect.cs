// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Numerics;

namespace Doc.LAMA0265.Error;

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
        // Error: T is a compile-time type parameter, so its static interface members cannot be accessed.
        Console.WriteLine( T.Zero );
    }
}
