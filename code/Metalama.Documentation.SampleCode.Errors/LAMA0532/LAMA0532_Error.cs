// This is public domain Metalama sample code.

namespace Doc.LAMA0532.Error;

// Error: the [Validatable] aspect introduces an abstract method, but the class isn't abstract.
[Validatable]
internal partial class Entity
{
    public int Id { get; set; }
}
