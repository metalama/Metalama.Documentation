// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.WellKnownRegexContracts;

public class Customer
{
    [Phone]
    public string? Phone { get; set; }

    [Email]
    public string? Email { get; set; }

    [Url]
    public string? Profile { get; set; }
}