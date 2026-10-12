// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA1004.Fixed;

internal class RedirectPrintsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The Printer class is declared in the source code, so its methods can be intercepted.
        builder.InterceptMethods(
            b => b
                .Type( typeof(Printer) )
                .Methods( "Print" )
                .RedirectToExistingMethod( typeof(Interceptors).AsINamedType().Methods.OfName( "Print" ).Single() ) );
    }
}

internal static class Interceptors
{
    public static void Print( string value ) => Console.WriteLine( "intercepted: " + value );
}
