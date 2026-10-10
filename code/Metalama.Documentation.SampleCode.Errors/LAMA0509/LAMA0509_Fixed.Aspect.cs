// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0509.Fixed;

public class DescribeAttribute : TypeAspect
{
    // The aspect overrides the method when the type already declares it.
    [Introduce( WhenExists = OverrideStrategy.Override )]
    public string Describe()
    {
        Console.WriteLine( "Describing the object." );

        return meta.Proceed()!;
    }
}
