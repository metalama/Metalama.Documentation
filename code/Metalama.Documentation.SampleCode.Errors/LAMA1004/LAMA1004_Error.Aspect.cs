// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA1004.Error;

internal class RedirectPrintsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var printer = builder.IntroduceClass( "Printer" ).Declaration;

        // Error: the Printer class is introduced by this aspect, so its methods cannot be intercepted.
        builder.InterceptMethods(
            b => b
                .Type( printer )
                .Methods( "Print" )
                .RedirectToExistingMethod( typeof(Interceptors).AsINamedType().Methods.OfName( "Print" ).Single() ) );
    }
}

internal static class Interceptors
{
    public static void Print( string value ) => Console.WriteLine( "intercepted: " + value );
}
