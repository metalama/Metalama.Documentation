// This is public domain Metalama sample code.

using Metalama.Extensions.Metrics;
using Metalama.Framework.Aspects;
using Metalama.Framework.Metrics;
using System;

namespace Doc.LogComplexMethods;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Get the syntax node count at compile time directly from the template.
        var syntaxNodeCount = meta.Target.Method.Metrics().Get<SyntaxNodesCount>().Value;

        Console.WriteLine( $"Entering {meta.Target.Method.Name} (complexity: {syntaxNodeCount} syntax nodes)" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Leaving {meta.Target.Method.Name}" );
        }
    }
}
