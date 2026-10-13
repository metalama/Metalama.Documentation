// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.BindingByName;

public class RedirectMailAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(MailOutbox).AsINamedType()
            .Methods.OfName( nameof(MailOutbox.Send) )
            .Single();

        builder.InterceptMethods(
            calls => calls
                .Type( typeof(Mailer) )
                .Methods( nameof(Mailer.Send) )
                .RedirectToExistingMethod( interceptor ) );
    }
}

public static class MailOutbox
{
    // The parameters are in another order, and there is no parameter for the priority.
    // [<snippet Interceptor>]
    public static void Send( string subject, string recipient )
    // [<endsnippet Interceptor>]
        => Console.WriteLine( $"Queued '{subject}' for {recipient}." );
}
