// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;

namespace Doc.GenericLiftedTypeParameter;

public class TraceListChangesAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(List<>) )
                .Methods( nameof(List<object>.Add) )
                .RedirectToSynthesizedMethod( nameof(this.Trace) ) );

    // The generated class cannot name the type parameter T of Repository<T>,
    // so T becomes a type parameter of the generated method.
    [Template]
    public dynamic? Trace()
    {
        var itemType = meta.Interception.Method.InterceptedMethod.Parameters[0].Type;
        Console.WriteLine( $"Adding a {itemType.ToTypeOfExpression().Value!.Name}." );

        return meta.Proceed();
    }
}
