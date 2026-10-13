// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.LAMA1010.Fixed;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // A single registration returns an interceptor for the call.
        amender.InterceptMethods(
            b => b
                .Type( typeof(Printer) )
                .Methods( "Print" )
                .RedirectToExistingMethod( typeof(Interceptors).AsINamedType().Methods.OfName( "Log" ).Single() ) );
    }
}

internal static class Interceptors
{
    public static void Log( string value ) => Console.WriteLine( "log: " + value );

    public static void Trace( string value ) => Console.WriteLine( "trace: " + value );
}
