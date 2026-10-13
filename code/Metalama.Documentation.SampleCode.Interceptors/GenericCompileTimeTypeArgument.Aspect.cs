// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.GenericCompileTimeTypeArgument;

public class ValidateSerializationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(Serializer) )
                .Methods( nameof(Serializer.Serialize) )
                // [<snippet WithArgs>]
                .ForEachSite(
                    site => site
                        .RedirectToSynthesizedMethod( nameof(this.Validate) )
                        .WithArgs( new { T = site.InterceptedMethod.TypeArguments[0] } ) ) );
                // [<endsnippet WithArgs>]

    // T is a compile-time type parameter: the template generates different code for each
    // type argument.
    [Template]
    // [<snippet Template>]
    public dynamic? Validate<[CompileTime] T>()
    {
        if ( meta.CompileTime( typeof(T).IsValueType ) )
        {
            Console.WriteLine( $"Serializing a value of type {typeof(T).Name}." );
        }
        else
        {
            if ( meta.Interception.Method.ArgumentParameters[0].Value == null )
            {
                throw new ArgumentNullException( "value" );
            }
        }

        return meta.Proceed();
    }
    // [<endsnippet Template>]
}
