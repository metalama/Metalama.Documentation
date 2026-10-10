// This is public domain Metalama sample code.

namespace Doc.LAMA0503.Error;

internal class Document
{
    public virtual long GetVersion() => 1;
}

// Error: the introduced GetVersion method returns int, but the base method returns long.
[Versioned]
internal partial class Contract : Document { }
