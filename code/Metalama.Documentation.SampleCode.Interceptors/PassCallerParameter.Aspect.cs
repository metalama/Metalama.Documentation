// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.PassCallerParameter;

public class AddRequestIdAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(AuditLog) )
                .Methods( nameof(AuditLog.Write) )
                .ForEachSite(
                    site =>
                    {
                        // [<snippet Guard>]
                        var requestId = site.Caller.Parameters.FirstOrDefault( p => p.Name == "requestId" );

                        // A static lambda cannot read the parameter, and a lambda cannot capture a parameter passed by reference.
                        if ( requestId == null
                             || (site.Caller.IsInNestedFunction && (site.Caller.IsInStaticFunction || requestId.RefKind != RefKind.None)) )
                        {
                            return site.Skip();
                        }

                        return site
                            .RedirectToExistingMethod(
                                typeof(AuditLogWithRequest).AsINamedType().Methods.OfName( nameof(AuditLogWithRequest.Write) ).Single() )
                            .WithArgs( new { requestId } );
                        // [<endsnippet Guard>]
                    } ) );
}

public static class AuditLogWithRequest
{
    public static void Write( string message, string requestId ) => AuditLog.Write( $"[{requestId}] {message}" );
}
