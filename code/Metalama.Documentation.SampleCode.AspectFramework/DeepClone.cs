// This is public domain Metalama sample code.

using System;

namespace Doc.DeepClone;

internal class ManuallyCloneable : ICloneable
{
    public object Clone()
    {
        return new ManuallyCloneable();
    }
}

[DeepClone]
internal class AutomaticallyCloneable
{
    private int _a;
    private ManuallyCloneable? _b;
    private AutomaticallyCloneable? _c;
}

internal class DerivedCloneable : AutomaticallyCloneable
{
    private string? _d;
}