// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5006.Error;

public class OrderLine
{
    // Warning: a uint value can never be negative.
    [NonNegative]
    public uint Quantity { get; set; }

    [NonNegative]
    public decimal UnitPrice { get; set; }
}
