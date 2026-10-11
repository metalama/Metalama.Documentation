// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0503.Error;

public class VersionedAttribute : TypeAspect
{
    // The template returns int, but Document.GetVersion returns long.
    [Introduce( WhenExists = OverrideStrategy.Override )]
    public int GetVersion()
    {
        Console.WriteLine( "Getting the version." );

        return meta.Proceed();
    }
}
