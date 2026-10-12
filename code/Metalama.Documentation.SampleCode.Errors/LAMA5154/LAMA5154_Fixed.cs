// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5154.Fixed;

[Observable]
public partial class Customer
{
    public string? Name { get; set; }

    // Fixed: the property is no longer virtual.
    public string? Email { get; set; }
}
