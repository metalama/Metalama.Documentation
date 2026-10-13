// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.InterfaceCalls;

public class LogRepositoryCallsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            // [<snippet Selection>]
            calls => calls
                .Type( typeof(IOrderRepository) )
                .Methods( nameof(IOrderRepository.Save) )
                .IncludingInterfaceImplementations()
                .RedirectToSynthesizedMethod( nameof(this.LogCall) ) );
            // [<endsnippet Selection>]

    [Template]
    public dynamic? LogCall()
    {
        Console.WriteLine(
            $"Calling {meta.Interception.Method.InterceptedMethod.ToDisplayString()}." );

        return meta.Proceed();
    }
}
