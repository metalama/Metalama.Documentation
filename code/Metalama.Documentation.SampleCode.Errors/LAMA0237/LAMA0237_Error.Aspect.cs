// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0237.Error;

public abstract class ClockAttribute : TypeAspect
{
    // Error: an abstract template cannot have the run-time-only type IClock.
    [Introduce]
    public abstract IClock Clock { get; }
}
