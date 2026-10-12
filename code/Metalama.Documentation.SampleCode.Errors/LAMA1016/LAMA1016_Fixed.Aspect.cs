// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA1016.Fixed;

public class InterceptPrintAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            i => i
                .Type( typeof(Printer) ).Methods( "Print" )
                .RedirectToSynthesizedMethod( nameof(this.Intercept) )
                // Fixed: args gives the value of the added parameter.
                .WithArgs( new { line = 42 } )
                .PlaceInCallingType()
                .Configure( b =>
                    {
                        b.Method.AddParameter( "line", typeof(int) );
                    } ) );

    [Template]
    public dynamic? Intercept()
    {
        Console.Write( $"[line {meta.Target.Parameters["line"].Value}] " );

        return meta.Proceed();
    }
}
