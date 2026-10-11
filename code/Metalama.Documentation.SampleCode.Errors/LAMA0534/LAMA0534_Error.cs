// This is public domain Metalama sample code.

namespace Doc.LAMA0534.Error;

// Error: the [Versioned] aspect introduces a field, but an interface can't contain instance fields.
[Versioned]
internal interface IDocument
{
    string Title { get; }
}

internal class Document : IDocument
{
    public string Title { get; set; } = "";
}
