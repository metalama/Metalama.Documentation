// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;
using System.Net.Http;

namespace Doc.LogHttpCalls;

public class LogHttpCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(HttpLogging).AsINamedType().Methods.OfName( nameof(HttpLogging.GetStringAsync) ).Single();

        // [<snippet Registration>]
        builder.InterceptMethods(
            calls => calls
                .Type( typeof(HttpClient) )
                .Methods( nameof(HttpClient.GetStringAsync) )
                .Where( m => m.Parameters is [{ Type.SpecialType: SpecialType.String }] )
                .RedirectToExistingMethod( interceptor ) );
        // [<endsnippet Registration>]
    }
}
