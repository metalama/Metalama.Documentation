// This is public domain Metalama sample code.

namespace Doc.LAMA0651.Fixed;

[CustomHashCode]
public record Customer
{
    public string Name { get; init; } = "";

    public string Email { get; init; } = "";
}
