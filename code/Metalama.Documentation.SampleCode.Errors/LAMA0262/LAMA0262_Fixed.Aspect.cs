// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0262.Fixed;

public class VersionedAttribute : TypeAspect
{
    // Fixed: the [Introduce] attribute is applied to the property.
    [Introduce]
    public int SchemaVersion
    {
        get => 1;
    }
}
