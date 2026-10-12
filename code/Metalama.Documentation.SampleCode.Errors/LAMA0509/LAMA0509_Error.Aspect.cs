// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0509.Error;

public class DescribeAttribute : TypeAspect
{
    // The aspect introduces the method with OverrideStrategy.New.
    [Introduce( WhenExists = OverrideStrategy.New )]
    public string Describe()
    {
        Console.WriteLine( "Describing the object." );

        return meta.Proceed()!;
    }
}
