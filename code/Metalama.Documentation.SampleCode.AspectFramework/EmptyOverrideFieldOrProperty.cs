// This is public domain Metalama sample code.

namespace Doc;

internal class EmptyOverrideFieldOrPropertyExample
{
    [EmptyOverrideFieldOrProperty]
    public int Field;

    [EmptyOverrideFieldOrProperty]
    public string? Property { get; set; }
}