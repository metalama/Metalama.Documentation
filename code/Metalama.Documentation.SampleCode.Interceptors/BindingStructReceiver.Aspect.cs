// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.BindingStructReceiver;

public class TraceCountersAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(CounterTracing).AsINamedType().Methods.OfName( nameof(CounterTracing.Increment) ).Single();

        builder.InterceptMethods(
            calls => calls
                .Type( typeof(Counter) )
                .Methods( nameof(Counter.Increment) )
                .RedirectToExistingMethod( interceptor ) );
    }
}

public static class CounterTracing
{
    // Increment modifies the struct, so the receiver must be passed by reference.
    public static void Increment( ref Counter counter )
    {
        counter.Add( 1 );
        Console.WriteLine( $"Counter incremented to {counter.Value}." );
    }
}
