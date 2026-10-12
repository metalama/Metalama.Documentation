// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA1017.Fixed;

public class InterceptProcessAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            i => i
                .Type( typeof(OrderService) ).Methods( "Process" )
                // Fixed: the method is generated in OrderService, which can access the private type Order.
                .RedirectToSynthesizedMethod( nameof(this.Intercept) )
                .PlaceInCallingType() );

    [Template]
    public dynamic? Intercept() => meta.Proceed();
}
