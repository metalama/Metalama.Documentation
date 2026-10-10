// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5154.Error;

[Observable]
public partial class Customer
{
    public string? Name { get; set; }

    // Error: the [Observable] aspect doesn't support virtual properties.
    public virtual string? Email { get; set; }
}
