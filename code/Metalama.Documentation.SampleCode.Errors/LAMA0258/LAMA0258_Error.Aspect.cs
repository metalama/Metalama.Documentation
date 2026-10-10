// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0258.Error;

public class TrackFinalizationAttribute : TypeAspect
{
    // Error: a finalizer can't be a template.
    [Introduce]
    ~TrackFinalizationAttribute()
    {
        Console.WriteLine( "Finalizing the object." );
    }
}
