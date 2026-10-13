// This is public domain Metalama sample code.

namespace Doc.LAMA0512.Error;

// Error: Product already implements IDescribable.
[Describable]
internal partial class Product : IDescribable
{
    public string Describe() => "A product";
}

[Describable]
internal partial class Customer { }
