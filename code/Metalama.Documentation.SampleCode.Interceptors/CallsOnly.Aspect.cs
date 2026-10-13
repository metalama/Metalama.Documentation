// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.CallsOnly;

public class LogTextCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(TextUtil) )
                .AllMethods()
                // [<snippet CallsOnly>]
                .ForEachSite(
                    site => site.UseKind == MethodUseKind.Call
                        ? site.RedirectToSynthesizedMethod( nameof(this.LogCall) )
                        : site.Skip() ) );
                // [<endsnippet CallsOnly>]

    [Template]
    public dynamic? LogCall()
    {
        Console.WriteLine( $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
