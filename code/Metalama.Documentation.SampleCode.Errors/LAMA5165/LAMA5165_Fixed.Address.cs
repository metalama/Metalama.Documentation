// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5165.Fixed;

[Observable]
public partial class Address
{
    public string City { get; set; } = "";
}
