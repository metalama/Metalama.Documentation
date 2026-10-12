// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0262.Error;

public class VersionedAttribute : TypeAspect
{
    public int SchemaVersion
    {
        // Error: the [Introduce] attribute cannot be applied to an accessor.
        [Introduce]
        get => 1;
    }
}
