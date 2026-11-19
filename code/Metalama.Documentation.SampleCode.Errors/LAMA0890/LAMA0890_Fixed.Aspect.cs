// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Doc.LAMA0890.Fixed;

[CompileTime]
internal static class LogSettings
{
    // Fixed: an ImmutableArray<string> can't be modified once it's created.
    public static readonly ImmutableArray<string> ExcludedMethods = ImmutableArray.Create( "Dispose", "ToString" );
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
