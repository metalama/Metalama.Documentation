// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5155.Error;

[Observable]
public partial class Product
{
    public decimal Price { get; set; }
}

// The [Observable] aspect is inherited from Product.
public partial class DiscountedProduct : Product
{
    // Error: the property hides the Price property of the base class.
    public new decimal Price { get; set; }
}
