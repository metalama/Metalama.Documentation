// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0534.Error;

public class VersionedAttribute : TypeAspect
{
    // Stores the version number of the entity.
    [Introduce]
    private int _version;
}
