// This is public domain Metalama sample code.

using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA1000.Fixed;

public class RedirectLoggingAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The interceptor is registered on the target of the aspect.
        builder.InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() ) );
    }
}
