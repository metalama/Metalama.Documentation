// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5007.Fixed;

public class Product
{
    // Fixed: [StrictlyPositive] states explicitly that zero isn't allowed.
    [StrictlyPositive]
    public decimal Price { get; set; }
}
