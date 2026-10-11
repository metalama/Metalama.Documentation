// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.LAMA1008.Error;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.InterceptMethods(
            b => b
                .Type( typeof(Printer) )
                .Methods( "Prnt" ) // Warning: the Printer class has no method named Prnt.
                .RedirectToExistingMethod( typeof(Interceptors).AsINamedType().Methods.OfName( "Print" ).Single() ) );
    }
}

internal static class Interceptors
{
    public static void Print( string value ) => Console.WriteLine( "intercepted: " + value );
}
