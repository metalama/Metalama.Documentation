// This is public domain Metalama sample code.

namespace Doc.LAMA0511.Error;

// Error: the Describe() template of the aspect doesn't have the return type of IDescribable.Describe().
[Describable]
internal partial class Product
{
    public decimal Price { get; set; }
}
