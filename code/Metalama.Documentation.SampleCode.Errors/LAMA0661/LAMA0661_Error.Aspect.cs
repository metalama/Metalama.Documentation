// This is public domain Metalama sample code.

using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0661.Error;

public class UseModernLoggerAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Redirect the calls of LegacyLogger.Write to Logger.Write, and pass the first parameter of the calling method.
        builder.InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .ForEachSite(
                    site => site
                        .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() )
                        .WithArgs( new { requestId = site.Caller.Parameters[0] } ) ) );
    }
}
