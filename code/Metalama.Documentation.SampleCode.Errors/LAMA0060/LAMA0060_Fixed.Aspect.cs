// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0060.Fixed;

public class CounterAttribute : TypeAspect
{
    [Introduce]
    private int _count;

    // Fixed: the property returns the value instead of a reference.
    [Introduce]
    public int Count => this._count;

    [Introduce]
    public void Increment() => this._count++;
}
