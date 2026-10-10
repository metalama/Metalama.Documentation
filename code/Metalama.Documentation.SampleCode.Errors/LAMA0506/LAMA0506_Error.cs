// This is public domain Metalama sample code.

namespace Doc.LAMA0506.Error;

// Error: the [Versioned] aspect introduces a Version property, but the class already has a Version field.
[Versioned]
public partial class Document
{
    public int Version = 1;

    public string? Title { get; set; }
}
