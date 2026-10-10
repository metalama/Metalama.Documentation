// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0252.Error;

public partial class DescribeAttribute : TypeAspect
{
    // Error: the introduced method is a template, so it cannot be partial.
    [Introduce]
    public partial void Describe();
}

public partial class DescribeAttribute
{
    public partial void Describe()
    {
        Console.WriteLine( $"This is a {meta.Target.Type.Name}." );
    }
}
