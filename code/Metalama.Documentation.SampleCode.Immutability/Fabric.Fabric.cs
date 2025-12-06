// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Immutability;
using Metalama.Patterns.Immutability.Configuration;
using System;

namespace Metalama.Documentation.SampleCode.Immutability.Fabric;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectReflectionType( typeof(Uri) ).ConfigureImmutability( ImmutabilityKind.Deep );
    }
}