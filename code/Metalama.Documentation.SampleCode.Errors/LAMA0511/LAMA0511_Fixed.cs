// This is public domain Metalama sample code.

namespace Doc.LAMA0511.Fixed;

// Fixed: the Describe() template of the aspect has the same return type as IDescribable.Describe().
[Describable]
internal partial class Product
{
    public decimal Price { get; set; }
}
