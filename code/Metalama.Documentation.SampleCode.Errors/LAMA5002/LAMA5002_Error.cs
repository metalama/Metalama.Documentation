// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5002.Error;

public class Customer
{
    // Warning: the [NotNull] contract contradicts the nullable type.
    [NotNull]
    public string? Email { get; set; }
}
