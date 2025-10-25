// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects.Configuration;
using System;

namespace Doc.AbsoluteExpiration_Fabric;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.Select( x => x.GlobalNamespace.GetDescendant( "MyProduct.MyNamespace" )! )
            .ConfigureCaching( caching => caching.AbsoluteExpiration =
                                   TimeSpan.FromMinutes( 20.0 ) );
    }
}