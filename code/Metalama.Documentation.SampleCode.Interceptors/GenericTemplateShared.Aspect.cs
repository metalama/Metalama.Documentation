// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.GenericTemplateShared;

public class TraceSerializationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(Serializer) )
                .Methods( nameof(Serializer.Serialize) )
                .RedirectToSynthesizedMethod( nameof(this.Trace) ) );

    // T is a run-time type parameter, so the generated method is generic and all call sites share it.
    [Template]
    public string Trace<T>( T value )
    {
        Console.WriteLine( $"Serializing a {typeof(T).Name}." );

        return meta.Proceed()!;
    }
}
