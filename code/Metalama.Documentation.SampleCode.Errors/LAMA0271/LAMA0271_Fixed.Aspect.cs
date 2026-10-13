// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0271.Fixed;

public class ExtensionsAttribute : TypeAspect
{
    // Fixed: the [This] attribute makes the introduced method an extension method.
    [Introduce]
    public static void PrintDetails( [This] object self )
    {
        Console.WriteLine( self );
    }
}
