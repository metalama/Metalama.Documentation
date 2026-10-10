// This is public domain Metalama sample code.

namespace Doc.LAMA0531.Fixed;

[Memento]
internal partial class Document
{
    public string? Text { get; set; }

    // Fixed: the [Memento] aspect introduces a nested type with another name.
    public class Snapshot
    {
        public int Version { get; set; }
    }
}
