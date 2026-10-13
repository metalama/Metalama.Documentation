// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0257.Error;

public class TrackableAttribute : TypeAspect
{
    // This property is a template: it describes a property that the aspect introduces into the target type.
    [Introduce]
    public string Category { get; set; } = "General";
}
