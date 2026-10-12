// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.StringLengthContract;

public class Customer
{
    [StringLength( 12, 64 )]
    public string? Password { get; set; }
}