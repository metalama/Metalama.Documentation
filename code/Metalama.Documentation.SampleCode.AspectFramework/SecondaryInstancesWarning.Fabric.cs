// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.SecondaryInstancesWarning;

// A NamespaceFabric applies to all types within the namespace it's defined in.
internal class Fabric : NamespaceFabric
{
    public override void AmendNamespace( INamespaceAmender amender )
    {
        // Add the Log aspect to all public methods in this namespace.
        amender
            .SelectTypes()
            .SelectMany( t => t.Methods )
            .Where( m => m.Accessibility == Accessibility.Public && m.Name != "ToString" )
            .AddAspectIfEligible<LogAttribute>();
    }
}
