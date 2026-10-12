// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.LAMA1008.Fixed;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.InterceptMethods(
            b => b
                .Type( typeof(Printer) )
                .Methods( "Print" ) // The name matches the Print method of the Printer class.
                .RedirectToExistingMethod( typeof(Interceptors).AsINamedType().Methods.OfName( "Print" ).Single() ) );
    }
}

internal static class Interceptors
{
    public static void Print( string value ) => Console.WriteLine( "intercepted: " + value );
}
