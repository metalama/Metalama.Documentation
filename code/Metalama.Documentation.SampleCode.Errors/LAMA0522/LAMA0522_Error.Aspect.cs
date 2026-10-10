// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0522.Error;

public class TrackFinalizationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: a finalizer cannot be introduced with the New strategy.
        builder.IntroduceFinalizer( nameof(Finalizer), whenExists: OverrideStrategy.New );
    }

    [Template]
    public dynamic? Finalizer()
    {
        Console.WriteLine( $"Finalizing an instance of {meta.Target.Type}." );

        return meta.Proceed();
    }
}
