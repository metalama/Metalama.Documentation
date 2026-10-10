// This is public domain Metalama sample code.

namespace Doc.LAMA0115.Fixed;

internal class Customer
{
    // Fixed: the template of [LogRead] uses meta.Target.FieldOrProperty.
    [LogRead]
    public string? Name { get; set; }
}
