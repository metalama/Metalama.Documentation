// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.CreditCardContract;

public class Customer
{
    [CreditCard]
    public string? CreditCard { get; set; }
}