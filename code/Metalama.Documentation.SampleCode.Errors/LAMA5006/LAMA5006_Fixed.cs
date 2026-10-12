// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5006.Fixed;

public class OrderLine
{
    // Fixed: the uint type already excludes negative values, so the contract is removed.
    public uint Quantity { get; set; }

    [NonNegative]
    public decimal UnitPrice { get; set; }
}
