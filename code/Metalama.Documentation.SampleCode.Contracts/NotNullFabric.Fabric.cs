// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.NotNullFabric;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.VerifyNotNullableDeclarations();
    }
}