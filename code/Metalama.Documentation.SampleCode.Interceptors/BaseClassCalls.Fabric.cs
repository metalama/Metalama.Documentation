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
                    .RedirectToSynthesizedMethod( nameof(StyleTemplates.LogStyles) )
                    .WithTemplateProvider( TemplateProvider.FromInstance( new StyleTemplates() ) )
                    // A protected method can be called only from the class hierarchy,
                    // so the interceptor is generated in the common base class of the project.
                    .PlaceInType( typeof(AppControl) )
                    // C# allows the call of a protected method only through an instance of the calling class.
                    .Configure( method => method.Method.Parameters[0].Type = method.Method.DeclaringType ) );
            // [<endsnippet Selection>]
}

internal class StyleTemplates : ITemplateProvider
{
    [Template]
    public dynamic? LogStyles()
    {
        Console.WriteLine( "Updating styles." );

        return meta.Proceed();
    }
}
