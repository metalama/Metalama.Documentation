// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0237.Fixed;

public class ClockAttribute : TypeAspect
{
    // Fixed: the template is virtual and has a default implementation, which derived aspects can override.
    [Introduce]
    public virtual IClock Clock => new SystemClock();
}
