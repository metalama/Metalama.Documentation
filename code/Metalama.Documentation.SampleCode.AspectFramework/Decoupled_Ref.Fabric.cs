﻿// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.Decoupled_Ref;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var declarations = amender.SelectDeclarationsWithAttribute( typeof(LogAttribute) );

        declarations
            .OfType<IMethod>()
            .AddAspectIfEligible( _ => new LogAspect() );

        declarations
            .OfType<IProperty>()
            .AddAspectIfEligible( _ => new LogAspect() );
    }
}
