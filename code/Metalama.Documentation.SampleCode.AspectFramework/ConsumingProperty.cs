// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.ConsumingProperty;

public class Log : OverrideMethodAspect
{
    public string? Category { get; set; }

    public override dynamic? OverrideMethod()
    {
        if ( !meta.Target.Project.TryGetProperty( "DefaultLogCategory", out var defaultCategory ) )
        {
            defaultCategory = "Default";
        }

        Console.WriteLine( $"{this.Category ?? defaultCategory}: Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}