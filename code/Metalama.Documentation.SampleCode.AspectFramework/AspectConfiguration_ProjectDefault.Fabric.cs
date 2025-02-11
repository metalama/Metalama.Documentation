// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Options;
using System.Diagnostics;
using System.Linq;

namespace Doc.AspectConfiguration_ProjectDefault;

// The project fabric configures the project at compile time.
public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender
            .SetOptions(
                new LoggingOptions { Category = "GeneralCategory", Level = TraceLevel.Info } );

        amender
            .Select(
                x => x.GlobalNamespace.GetDescendant(
                    "Doc.AspectConfiguration_ProjectDefault.ChildNamespace" )! )
            .SetOptions( new LoggingOptions() { Category = "ChildCategory" } );

        // Adds the aspect to all members.
        amender
            .SelectMany( c => c.Types.SelectMany( t => t.Methods ) )
            .AddAspectIfEligible<LogAttribute>();
    }
}