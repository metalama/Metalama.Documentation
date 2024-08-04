﻿using Metalama.Framework.Advising;
using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.Decoupled;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var declarations = amender.SelectDeclarationsWithAttribute<LogAttribute>();

        declarations
            .OfType<IMethod>()
            .AddAspectIfEligible<LogAspect>(
                m =>
                {
                    var attribute = m.Attributes.GetConstructedAttributesOfType<LogAttribute>().Single();

                    return new LogAspect( attribute );
                } );
        
        declarations
            .OfType<IProperty>()
            .AddAspectIfEligible<LogAspect>(
                m =>
                {
                    var attribute = m.Attributes.GetConstructedAttributesOfType<LogAttribute>().Single();

                    return new LogAspect( attribute );
                } );
    }
}
