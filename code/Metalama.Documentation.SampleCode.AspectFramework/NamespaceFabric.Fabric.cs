// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.NamespaceFabric_.TargetNamespace;

// A namespace fabric is placed directly in the namespace it affects.
// It applies to all types within that namespace and its descendants.
internal class Fabric : NamespaceFabric
{
    public override void AmendNamespace( INamespaceAmender amender )
    {
        // Add the Log aspect to all methods in this namespace.
        amender
            .SelectTypes()
            .SelectMany( t => t.Methods )
            .AddAspectIfEligible<Log>();
    }
}
