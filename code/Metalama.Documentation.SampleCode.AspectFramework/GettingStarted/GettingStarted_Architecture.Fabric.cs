// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;

namespace Doc.GettingStarted_Architecture;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        const string ns = "Doc.GettingStarted_Architecture.VerifiedNamespace";

        amender
            .Select( compilation => compilation.GlobalNamespace.GetDescendant( ns )! )
            .CanOnlyBeUsedFrom( r => r.Namespace( ns ) );
    }
}