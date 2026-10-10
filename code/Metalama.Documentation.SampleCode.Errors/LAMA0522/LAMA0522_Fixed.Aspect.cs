// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0522.Fixed;

public class TrackFinalizationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Fixed: Override introduces the finalizer, or overrides the existing one.
        builder.IntroduceFinalizer( nameof(Finalizer), whenExists: OverrideStrategy.Override );
    }

    [Template]
    public dynamic? Finalizer()
    {
        Console.WriteLine( $"Finalizing an instance of {meta.Target.Type}." );

        return meta.Proceed();
    }
}
