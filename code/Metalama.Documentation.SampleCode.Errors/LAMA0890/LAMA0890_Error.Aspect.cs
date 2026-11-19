// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LAMA0890.Error;

[CompileTime]
internal static class LogSettings
{
    // Warning: the field is read-only, but a List<string> can still be modified.
    public static readonly List<string> ExcludedMethods = new() { "Dispose", "ToString" };
}

public class LogAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var method in builder.Target.Methods.Where( m => !LogSettings.ExcludedMethods.Contains( m.Name ) ) )
        {
            builder.With( method ).Override( nameof(this.OverrideMethod) );
        }
    }

    [Template]
    public dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
