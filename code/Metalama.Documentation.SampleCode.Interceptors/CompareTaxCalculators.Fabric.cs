// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.CompareTaxCalculators;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var interceptor = typeof(TaxComparison).AsINamedType()
            .Methods.OfName( nameof(TaxComparison.ComputeTax) )
            .Single();

        amender.InterceptMethods(
            calls => calls
                .Type( typeof(LegacyTaxCalculator) )
                .Methods( nameof(LegacyTaxCalculator.ComputeTax) )
                .RedirectToExistingMethod( interceptor ) );
    }
}
