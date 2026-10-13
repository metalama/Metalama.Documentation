// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.SkipNonInterceptable;

public class TraceCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(Store) )
                .AllMethods()
                // [<snippet Skip>]
                .ForEachSite(
                    site => site.NonInterceptableReason != NonInterceptableReason.None
                        ? site.Skip()
                        : site.RedirectToSynthesizedMethod( nameof(this.Trace) ) ) );
                // [<endsnippet Skip>]

    [Template]
    public dynamic? Trace()
    {
        Console.WriteLine( $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
