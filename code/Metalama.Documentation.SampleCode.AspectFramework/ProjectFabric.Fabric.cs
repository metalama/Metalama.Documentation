// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.ProjectFabric_;

internal class Fabric : ProjectFabric
{
    // This method is the compile-time entry point of your project.
    // It executes within the compiler or IDE.
    public override void AmendProject( IProjectAmender project )
    {
        project.SelectMany( p => p.Types.SelectMany( t => t.Methods ) ).AddAspectIfEligible<Log>();
    }
}