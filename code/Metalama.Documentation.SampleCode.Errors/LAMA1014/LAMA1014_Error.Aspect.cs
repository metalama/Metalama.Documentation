// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA1014.Error;

public class InterceptLoggingAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            i => i
                .Type( typeof(Logger) )
                .Methods( "Write" )
                .RedirectToSynthesizedMethod( nameof(this.Intercept) )
                .PlaceInCallingType()
                .Configure(
                    b =>
                    {
                        // The type parameter has no type argument at the call site.
                        b.Method.Parameters[0].Type = b.Method.AddTypeParameter( "TValue" );
                    } ) );

    [Template]
    public dynamic? Intercept()
    {
        Console.WriteLine( "Writing a message." );

        return meta.Proceed();
    }
}
