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
                .PlaceInCallingType()
                .Configure( method => method.Method.IsStatic = false ) );

    [Template]
    public dynamic? LogCall()
    {
        // The interceptor is a private instance method of the calling type, so it can read the private field _log.
        meta.This._log.WriteLine( $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
