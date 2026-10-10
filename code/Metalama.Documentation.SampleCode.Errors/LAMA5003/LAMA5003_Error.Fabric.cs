// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.LAMA5003.Error;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Adds [NotNull] to all public non-nullable reference-typed parameters, fields, and properties.
        amender.VerifyNotNullableDeclarations();
    }
}
