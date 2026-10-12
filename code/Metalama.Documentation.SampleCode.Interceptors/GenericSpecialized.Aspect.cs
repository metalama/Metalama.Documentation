// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.GenericSpecialized;

public class TraceSerializationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(Serializer) )
                .Methods( nameof(Serializer.Serialize) )
                .RedirectToSynthesizedMethod( nameof(this.Trace) ) );

    // The template has no type parameter, so one method is generated for each type argument of the call sites.
    // The type argument is known when the template is expanded.
    [Template]
    public dynamic? Trace()
    {
        var typeArgument = meta.Interception.Method.InterceptedMethod.TypeArguments[0];
        Console.WriteLine( $"Serializing a value of type {typeArgument.ToDisplayString()}." );

        return meta.Proceed();
    }
}
