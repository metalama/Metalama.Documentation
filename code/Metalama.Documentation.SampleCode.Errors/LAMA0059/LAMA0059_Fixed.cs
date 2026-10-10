// This is public domain Metalama sample code.

namespace Doc.LAMA0059.Fixed;

// Fixed: the type of the property is given with typeof.
[Id( typeof(int) )]
internal partial class Order
{
    public string? Customer { get; set; }
}
