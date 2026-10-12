// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0257.Fixed;

public class TrackableAttribute : TypeAspect
{
    // This property configures the aspect. It is not a template, so the attribute can set it.
    public string CategoryName { get; set; } = "General";

    // This template reads the configuration to compute the introduced property.
    [Introduce]
    public string Category => this.CategoryName;
}
