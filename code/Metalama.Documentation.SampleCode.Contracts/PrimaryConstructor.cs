// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.PrimaryConstructor;

public class Customer( [Required] string name, [Phone] string? phone = null )
{
    public string Name { get; } = name;

    public string? Phone { get; } = phone;
}
