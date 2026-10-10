// This is public domain Metalama sample code.

namespace Doc.LAMA0223.Fixed;

internal class PriceCalculator
{
    [Log]
    public decimal GetPrice( int quantity ) => quantity * 9.99m;
}
