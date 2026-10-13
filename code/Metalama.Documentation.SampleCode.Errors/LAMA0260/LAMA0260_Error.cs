// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0260.Error;

[RunTimeOrCompileTime]
internal class PriceCalculator
{
    public decimal ApplyDiscount( decimal price, decimal rate ) => price * (1 - rate);

    public decimal AddTax( decimal price, decimal rate ) => price * (1 + rate);

    // Error: a type fabric cannot be nested in a run-time-or-compile-time type.
    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            amender.SelectMany( t => t.Methods ).AddAspect<LogAttribute>();
        }
    }
}
