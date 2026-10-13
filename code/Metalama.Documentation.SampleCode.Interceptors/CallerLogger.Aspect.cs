// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.CallerLogger;

public class LogPaymentCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(PaymentGateway) )
                .AllMethods()
                .RedirectToSynthesizedMethod( nameof(this.LogCall) )
                // [<snippet Placement>]
                .PlaceInCallingType() );
                // [<endsnippet Placement>]

    [Template]
    public dynamic? LogCall()
    {
        // The interceptor is a private method of the calling type, so it can read
        // the private static field _log.
        // [<snippet This>]
        meta.ThisType._log.WriteLine(
            $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );
        // [<endsnippet This>]

        return meta.Proceed();
    }
}
