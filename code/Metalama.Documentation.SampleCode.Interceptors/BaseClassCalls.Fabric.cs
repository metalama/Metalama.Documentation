// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;

namespace Doc.BaseClassCalls;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender
            // [<snippet Selection>]
            .SelectTypes()
            .Where( t => t.IsConvertibleTo( typeof(AppControl) ) )
            .InterceptMethods(
                calls => calls
                    // Select the framework class that declares the protected method.
                    .Type( typeof(Control) )
                    .Methods( "UpdateStyles" )
                    .RedirectToSynthesizedMethod( nameof(this.LogStyles) )
                    // A protected method can be called only from the class hierarchy,
                    // so the interceptor is generated in the common base class of the project.
                    .PlaceInType( typeof(AppControl) ) );
            // [<endsnippet Selection>]

    [Template]
    public dynamic? LogStyles()
    {
        Console.WriteLine( "Updating styles." );

        return meta.Proceed();
    }
}
