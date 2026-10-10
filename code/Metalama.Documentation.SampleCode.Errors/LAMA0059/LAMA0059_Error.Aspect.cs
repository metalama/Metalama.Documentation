// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0059.Error;

// Error: a non-abstract aspect class cannot be generic.
public class IdAttribute<T> : TypeAspect
{
    [Introduce]
    public T? Id { get; set; }
}
