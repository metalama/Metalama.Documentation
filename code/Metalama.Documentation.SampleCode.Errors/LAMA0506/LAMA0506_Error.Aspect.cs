// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0506.Error;

public class VersionedAttribute : TypeAspect
{
    // The aspect introduces a property named Version.
    [Introduce]
    public int Version { get; set; }
}
