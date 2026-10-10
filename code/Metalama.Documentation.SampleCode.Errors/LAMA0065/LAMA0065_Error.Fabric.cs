// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Framework.Options;

namespace Doc.LAMA0065.Error;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Error: the options are also set on static types, which are not eligible.
        amender
            .SelectMany( c => c.Types )
            .SetOptions( new LoggingOptions { Category = "Calculation" } );
    }
}
