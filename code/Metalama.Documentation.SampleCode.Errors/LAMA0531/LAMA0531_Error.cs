// This is public domain Metalama sample code.

namespace Doc.LAMA0531.Error;

[Memento]
internal partial class Document
{
    public string? Text { get; set; }

    // Error: the [Memento] aspect tries to introduce another nested type named Snapshot.
    public class Snapshot
    {
        public int Version { get; set; }
    }
}
