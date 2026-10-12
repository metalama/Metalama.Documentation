// This is public domain Metalama sample code.

namespace Doc.LAMA0652.Fixed;

[TrackEquality]
public record Product
{
    public string Name { get; init; } = "";

    // The property is not virtual.
    public decimal Price { get; set; }
}
