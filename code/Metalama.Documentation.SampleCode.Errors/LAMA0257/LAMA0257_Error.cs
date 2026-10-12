// This is public domain Metalama sample code.

// This file intentionally has no Metalama using directive.

namespace Doc.LAMA0257.Error;

// Error: the Category property of the aspect is a template and can't be set from the attribute.
[Trackable( Category = "Orders" )]
internal partial class Order
{
    public int Id { get; set; }
}
