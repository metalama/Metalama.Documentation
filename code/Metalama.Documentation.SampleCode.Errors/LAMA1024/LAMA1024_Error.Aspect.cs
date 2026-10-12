// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA1024.Error;

public class InterceptPrintAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            i => i
                .Type( typeof(Printer) ).Methods( "Print" )
                .RedirectToSynthesizedMethod( nameof(this.Intercept) )
                .PlaceInType( typeof(Helpers) )
                .Configure( b =>
                    {
                        // Error: a private method of Helpers cannot be called from Report.
                        b.Method.Accessibility = Accessibility.Private;
                    } ) );

    [Template]
    public dynamic? Intercept()
    {
        Console.Write( "> " );

        return meta.Proceed();
    }
}
