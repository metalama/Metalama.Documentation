// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.TemplateAddedParameter;

public class TracePricingAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(PricingClient) )
                .AllMethods()
                .RedirectToSynthesizedMethod( nameof(this.Trace) )
                .WithArgs( new { category = "Pricing" } )
                .Configure(
                    method =>
                    {
                        var caller = method.Method.AddParameter( "caller", typeof(string) );
                        method.SetArgument( caller, (TypedConstant) method.Context.Caller.Member.Name );
                    } ) );

    // category is a run-time parameter given by WithArgs; caller is added by the Configure delegate.
    // Both become parameters of the generated method, whose value each call site passes.
    [Template]
    public dynamic? Trace( string category, string caller )
    {
        Console.WriteLine( $"[{category}] {caller} calls {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
