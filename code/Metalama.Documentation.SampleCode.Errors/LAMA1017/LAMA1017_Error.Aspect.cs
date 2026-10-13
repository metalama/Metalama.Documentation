// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA1017.Error;

public class InterceptProcessAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            i => i
                .Type( typeof(OrderService) ).Methods( "Process" )
                // Error: by default, the method is generated in an internal class, which cannot expose the private type Order.
                .RedirectToSynthesizedMethod( nameof(this.Intercept) ) );

    [Template]
    public dynamic? Intercept() => meta.Proceed();
}
