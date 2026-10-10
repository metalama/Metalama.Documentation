// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0500.Fixed;

public class ResettableAttribute : TypeAspect
{
    [Introduce( WhenExists = OverrideStrategy.Override )]
    public void Reset()
    {
        Console.WriteLine( "Resetting the object." );
        meta.Proceed();
    }
}
