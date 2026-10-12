// This is public domain Metalama sample code.

using System;
using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA1001.Fixed;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // The scope is the project, whose source code contains the calls to intercept.
        amender.InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() ) );
    }
}
