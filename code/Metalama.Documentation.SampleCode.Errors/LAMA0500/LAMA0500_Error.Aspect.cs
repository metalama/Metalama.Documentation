// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0500.Error;

public class ResettableAttribute : TypeAspect
{
    [Introduce]
    public void Reset()
    {
        Console.WriteLine( "Resetting the object." );
        meta.Proceed();
    }
}
