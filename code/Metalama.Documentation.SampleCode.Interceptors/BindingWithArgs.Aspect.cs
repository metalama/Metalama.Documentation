// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.BindingWithArgs;

public class AddMetricContextAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(MetricsWithContext).AsINamedType().Methods.OfName( nameof(MetricsWithContext.Increment) ).Single();

        builder.InterceptMethods(
            calls => calls
                .Type( typeof(Metrics) )
                .Methods( nameof(Metrics.Increment) )
                .RedirectToExistingMethod( interceptor )
                // [<snippet WithArgs>]
                .WithArgs(
                    new Dictionary<string, object?>
                    {
                        ["category"] = "Sales",          // A string literal.
                        ["weight"] = 2,                  // A number.
                        ["valueType"] = typeof(decimal),  // A type, written as typeof.
                        ["unit"] = null                  // null.
                    } ) );
                // [<endsnippet WithArgs>]
    }
}

public static class MetricsWithContext
{
    public static void Increment( string name, string category, int weight, Type valueType, string? unit )
        => Metrics.Increment( $"{category}.{name} x{weight} ({valueType.Name}{(unit == null ? "" : ", " + unit)})" );
}
