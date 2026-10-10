// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture;
using Metalama.Framework.Fabrics;
using System.IO;

namespace Doc.LAMA0906.Error;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectTypesDerivedFrom( typeof(TextReader) )
            .MustRespectNamingConvention( "*Reader" );
    }
}
