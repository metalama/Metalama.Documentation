// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.MultipleInstances;

// A NamespaceFabric applies to all types within the namespace it's defined in.
internal class Fabric : NamespaceFabric
{
    public override void AmendNamespace( INamespaceAmender amender )
    {
        // Add the Log aspect with default "Monitoring" category to all public methods.
        amender
            .SelectTypes()
            .Where( t => t.Accessibility == Accessibility.Public )
            .SelectMany( t => t.Methods )
            .Where( m => m.Accessibility == Accessibility.Public && m.Name != "ToString" )
            .AddAspectIfEligible<LogAttribute>();
    }
}
