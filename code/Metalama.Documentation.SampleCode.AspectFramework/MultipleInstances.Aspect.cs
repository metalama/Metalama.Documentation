// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.MultipleInstances;

public class LogAttribute : OverrideMethodAspect
{
    public string Category { get; set; } = "Monitoring";

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        // Merge categories from all instances (primary + secondary).
        var allCategories = new[] { this.Category }
            .Concat(
                builder.AspectInstance.SecondaryInstances
                    .Select( i => ((LogAttribute) i.Aspect).Category ) )
            .Distinct()
            .OrderBy( c => c );

        // Store merged categories for use in the template.
        builder.Tags = new { Categories = string.Join( ", ", allCategories ) };
    }

    public override dynamic? OverrideMethod()
    {
        var categories = (string) meta.Tags["Categories"]!;

        Console.WriteLine(
            $"[{categories}] Entering {meta.Target.Method.DeclaringType.Name}.{meta.Target.Method.Name}" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine(
                $"[{categories}] Leaving {meta.Target.Method.DeclaringType.Name}.{meta.Target.Method.Name}" );
        }
    }
}
