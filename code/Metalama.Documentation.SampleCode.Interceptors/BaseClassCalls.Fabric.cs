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
            .SelectTypes()
            .Where( t => t.IsConvertibleTo( typeof(Control) ) )
            .InterceptMethods(
                calls => calls
                    // Select the base class that declares the methods, not the derived class.
                    .Type( typeof(Control) )
                    .Methods( nameof(Control.Invalidate) )
                    .RedirectToSynthesizedMethod( nameof(RepaintTemplates.LogRepaint) )
                    .WithTemplateProvider( TemplateProvider.FromInstance( new RepaintTemplates() ) )
                    .WithGranularity( InterceptorGranularity.Member ) );
}

internal class RepaintTemplates : ITemplateProvider
{
    [Template]
    public dynamic? LogRepaint()
    {
        Console.WriteLine( $"Repaint requested by {meta.Interception.CallingType.Name}.{meta.Interception.CallingMember.Name}." );

        return meta.Proceed();
    }
}
