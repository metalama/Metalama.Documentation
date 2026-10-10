// This is public domain Metalama sample code.

namespace Doc.LAMA0508.Fixed;

// Fixed: the [Describe] aspect introduces a static method that is not sealed.
[Describe]
public partial class Product
{
    public string? Name { get; set; }
}
