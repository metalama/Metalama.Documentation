// This is public domain Metalama sample code.

using System.Diagnostics;

namespace Doc.LAMA0521.Error;

// The class already has a [DebuggerDisplay] attribute.
[DefaultDebuggerDisplay]
[DebuggerDisplay( "Product {Name} ({Price})" )]
internal class Product
{
    public string Name { get; set; } = "";

    public decimal Price { get; set; }
}

[DefaultDebuggerDisplay]
internal class Customer
{
    public string Name { get; set; } = "";
}
