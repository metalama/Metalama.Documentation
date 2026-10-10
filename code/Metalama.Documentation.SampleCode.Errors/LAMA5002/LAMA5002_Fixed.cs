// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5002.Fixed;

public class Customer
{
    // Fixed: the type is non-nullable, consistently with the [NotNull] contract.
    [NotNull]
    public string Email { get; set; } = "";
}
