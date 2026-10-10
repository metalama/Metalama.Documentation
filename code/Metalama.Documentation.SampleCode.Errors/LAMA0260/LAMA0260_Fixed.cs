// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0260.Fixed;

// Fixed: the class no longer has the [RunTimeOrCompileTime] attribute, so it's a run-time type.
internal class PriceCalculator
{
    public decimal ApplyDiscount( decimal price, decimal rate ) => price * (1 - rate);

    public decimal AddTax( decimal price, decimal rate ) => price * (1 + rate);

    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            amender.SelectMany( t => t.Methods ).AddAspect<LogAttribute>();
        }
    }
}
