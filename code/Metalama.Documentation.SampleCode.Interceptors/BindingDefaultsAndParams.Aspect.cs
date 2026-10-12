// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.BindingDefaultsAndParams;

public class RedirectLoggingAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(StructuredLog).AsINamedType().Methods.OfName( nameof(StructuredLog.Write) ).Single();

        builder.InterceptMethods(
            calls => calls
                .Type( typeof(Log) )
                .Methods( nameof(Log.Write) )
                .RedirectToExistingMethod( interceptor ) );
    }
}

public static class StructuredLog
{
    // The severity has no default value, and the arguments are a plain array.
    public static void Write( string format, Severity severity, object[] args )
        => Console.WriteLine( $"[{severity}] {format} ({args.Length} arguments)" );
}
