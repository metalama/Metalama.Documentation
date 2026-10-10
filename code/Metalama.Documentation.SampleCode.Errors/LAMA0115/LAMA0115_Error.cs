// This is public domain Metalama sample code.

namespace Doc.LAMA0115.Error;

internal class Customer
{
    // Error: the template of [LogRead] uses meta.Target.Field, but Name is a property.
    [LogRead]
    public string? Name { get; set; }
}
