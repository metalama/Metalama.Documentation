// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.NumericContracts;

public class OrderLine
{
    [NonNegative]
    public decimal NominalPrice { get; }

    [StrictlyPositive]
    public decimal Quantity { get; }

    [Range( 0, 100 )]
    public int Discount { get; }
}