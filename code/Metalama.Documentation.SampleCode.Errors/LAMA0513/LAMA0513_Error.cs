// This is public domain Metalama sample code.

namespace Doc.LAMA0513.Error;

// Error: the aspect implements IEntity<T> without specifying the type argument.
[Entity]
internal partial class Product
{
    public decimal Price { get; set; }
}
