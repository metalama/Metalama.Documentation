// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0503.Fixed;

public class VersionedAttribute : TypeAspect
{
    // The template returns long, like Document.GetVersion.
    [Introduce( WhenExists = OverrideStrategy.Override )]
    public long GetVersion()
    {
        Console.WriteLine( "Getting the version." );

        return meta.Proceed();
    }
}
