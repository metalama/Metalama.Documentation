// This is public domain Metalama sample code.

namespace Doc.LAMA0509.Error;

// Error: Product already declares Describe(), so OverrideStrategy.New can't hide it.
[Describe]
internal partial class Product
{
    public string Describe() => "Product";
}
