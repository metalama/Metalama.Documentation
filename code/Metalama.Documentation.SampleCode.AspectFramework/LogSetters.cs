// This is public domain Metalama sample code.

namespace Doc.LogSetters;

[LogSetters]
internal partial class Person
{
    public string? Name { get; set; }

    public int Age { get; set; }

    // This property has no setter, so the aspect skips it.
    public string FullName => $"{Name} ({Age})";
}
