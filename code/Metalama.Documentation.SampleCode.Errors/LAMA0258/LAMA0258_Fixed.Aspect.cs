// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0258.Fixed;

public class TrackFinalizationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Fixed: the finalizer is introduced programmatically from a template method.
        builder.IntroduceFinalizer( nameof(this.FinalizerTemplate) );
    }

    [Template]
    private void FinalizerTemplate()
    {
        Console.WriteLine( "Finalizing the object." );
    }
}
