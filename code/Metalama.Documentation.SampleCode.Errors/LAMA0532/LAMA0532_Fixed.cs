// This is public domain Metalama sample code.

namespace Doc.LAMA0532.Fixed;

// Fixed: the class is abstract, so it can receive the abstract method.
[Validatable]
internal abstract partial class Entity
{
    public int Id { get; set; }
}
