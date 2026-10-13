// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.SkipTestHelpers;

public class LogPaymentCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(PaymentGateway) )
                .AllMethods()
                // [<snippet ForEachSite>]
                .ForEachSite(
                    site =>
                    {
                        var callingType = site.Caller.Member.DeclaringType;

                        if ( callingType.Attributes.Any( typeof(TestHelperAttribute) ) )
                        {
                            return site.Skip();
                        }

                        return site.RedirectToSynthesizedMethod( nameof(this.LogCall) );
                    } ) );
                // [<endsnippet ForEachSite>]

    [Template]
    public dynamic? LogCall()
    {
        Console.WriteLine( $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
