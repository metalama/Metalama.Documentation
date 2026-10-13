// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.MethodGroups;

public class LogTextCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(TextLogging).AsINamedType()
            .Methods.OfName( nameof(TextLogging.Normalize) )
            .Single();

        builder.InterceptMethods(
            calls => calls
                .Type( typeof(TextUtil) )
                .Methods( nameof(TextUtil.Normalize) )
                .RedirectToExistingMethod( interceptor ) );
    }
}
