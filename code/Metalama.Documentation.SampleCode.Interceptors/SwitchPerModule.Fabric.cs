// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.SwitchPerModule.Billing;

// A namespace fabric: its scope is the Billing namespace. Add one to each migrated module.
// [<snippet Fabric>]
internal class MigrationFabric : NamespaceFabric
{
    public override void AmendNamespace( INamespaceAmender amender )
    {
        var adapter = typeof(NewTaxCalculator).AsINamedType()
            .Methods.OfName( nameof(NewTaxCalculator.ComputeTax) )
            .Single( m => m.Parameters.Count == 3 );

        amender.InterceptMethods(
            calls => calls
                .Type( typeof(LegacyTaxCalculator) )
                .Methods( nameof(LegacyTaxCalculator.ComputeTax) )
                .RedirectToExistingMethod( adapter ) );
    }
}
// [<endsnippet Fabric>]
