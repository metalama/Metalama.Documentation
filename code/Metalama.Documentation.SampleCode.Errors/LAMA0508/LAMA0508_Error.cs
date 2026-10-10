// This is public domain Metalama sample code.

namespace Doc.LAMA0508.Error;

// Error: the [Describe] aspect introduces a method that is both static and sealed.
[Describe]
public partial class Product
{
    public string? Name { get; set; }
}
