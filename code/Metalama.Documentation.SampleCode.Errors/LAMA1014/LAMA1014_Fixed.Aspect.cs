// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA1014.Fixed;

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
                        var typeParameter = b.Method.AddTypeParameter( "TValue" );
                        b.Method.Parameters[0].Type = typeParameter;

                        // The type argument is the type of the argument at the call site.
                        b.SetTypeArgument( typeParameter, b.Context.Arguments[0].Type! );
                    } ) );

    [Template]
    public dynamic? Intercept()
    {
        Console.WriteLine( "Writing a message." );

        return meta.Proceed();
    }
}
