// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0060.Error;

public class CounterAttribute : TypeAspect
{
    [Introduce]
    private int _count;

    // Error: a template property cannot return a reference.
    [Introduce]
    public ref int Count => ref this._count;
}
