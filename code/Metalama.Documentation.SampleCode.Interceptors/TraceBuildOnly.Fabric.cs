// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.IO;

namespace Doc.TraceBuildOnly;

internal class TracingFabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Interceptors are registered only when the TRACE_CALLS symbol is defined.
        // [<snippet Condition>]
        if ( !amender.Project.PreprocessorSymbols.Contains( "TRACE_CALLS" ) )
        {
            return;
        }
        // [<endsnippet Condition>]

        amender.InterceptMethods(
            calls => calls
                .Type( typeof(File) )
                .Methods( nameof(File.ReadAllText) )
                .RedirectToSynthesizedMethod( nameof(TracingTemplates.Trace) )
                .WithTemplateProvider( TemplateProvider.FromInstance( new TracingTemplates() ) ) );
    }
}

internal class TracingTemplates : ITemplateProvider
{
    [Template]
    public dynamic? Trace()
    {
        Console.WriteLine( $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
