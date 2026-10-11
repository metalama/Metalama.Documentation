// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5155.Fixed;

[Observable]
public partial class Product
{
    public decimal Price { get; set; }
}

// The [Observable] aspect is inherited from Product.
public partial class DiscountedProduct : Product
{
    // Fixed: the property has its own name instead of hiding Price.
    public decimal DiscountedPrice { get; set; }
}
