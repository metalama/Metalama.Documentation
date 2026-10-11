// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0652.Error;

public class TrackEqualityAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Overriding the Equals method that the compiler synthesizes makes the linker generate its original implementation.
        builder.IntroduceMethod( nameof(IntroducedEquals), whenExists: OverrideStrategy.Override, args: new { T = builder.Target } );
    }

    [Template( Name = "Equals" )]
    public bool IntroducedEquals<[CompileTime] T>( T? other )
    {
        Console.WriteLine( "Comparing records." );

        return meta.Proceed();
    }
}
