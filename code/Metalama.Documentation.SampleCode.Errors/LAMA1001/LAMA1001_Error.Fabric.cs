// This is public domain Metalama sample code.

using System;
using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA1001.Error;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // The scope is the Console class, which belongs to a referenced assembly.
        amender.SelectReflectionType( typeof(Console) ).InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() ) );
    }
}
