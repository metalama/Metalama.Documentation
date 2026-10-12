// This is public domain Metalama sample code.

using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA1000.Error;

public class RedirectLoggingAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The aspect is applied to Checkout, but it registers the interceptor on OrderService.
        builder.With( typeof(OrderService).AsINamedType() ).InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() ) );
    }
}
