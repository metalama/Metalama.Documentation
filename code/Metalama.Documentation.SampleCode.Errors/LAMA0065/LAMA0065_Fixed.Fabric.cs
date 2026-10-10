// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Framework.Options;
using System.Linq;

namespace Doc.LAMA0065.Fixed;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Fixed: the options are set only on non-static types.
        amender
            .SelectMany( c => c.Types.Where( t => !t.IsStatic ) )
            .SetOptions( new LoggingOptions { Category = "Calculation" } );
    }
}
