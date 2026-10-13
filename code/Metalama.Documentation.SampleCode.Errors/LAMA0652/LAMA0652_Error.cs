// This is public domain Metalama sample code.

namespace Doc.LAMA0652.Error;

[TrackEquality]
public record Product
{
    public string Name { get; init; } = "";

    // The property is virtual, so a derived type can override it.
    public virtual decimal Price { get; set; }
}
