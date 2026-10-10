// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5007.Error;

public class Product
{
    // Warning: [Positive] doesn't say whether zero is allowed.
    [Positive]
    public decimal Price { get; set; }
}
