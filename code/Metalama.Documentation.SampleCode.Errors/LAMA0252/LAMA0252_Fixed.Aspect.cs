// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0252.Fixed;

public class DescribeAttribute : TypeAspect
{
    // Fixed: the template is declared and implemented in a single, non-partial method.
    [Introduce]
    public void Describe()
    {
        Console.WriteLine( $"This is a {meta.Target.Type.Name}." );
    }
}
