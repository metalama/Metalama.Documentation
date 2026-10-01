// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture;
using Metalama.Framework.Fabrics;
using System.IO;

namespace Doc.Architecture.NamingConvention_Fabric;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectTypesDerivedFrom( typeof(TextReader) )
            .MustRespectNamingConvention( "*Reader" );
    }
}

// The naming convention is broken.
internal class TextLoader : TextReader { }