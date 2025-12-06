// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.OverrideConstructor;

internal class LogConstructorsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var constructor in builder.Target.Constructors )
        {
            builder.With( constructor ).Override( nameof(this.OverrideConstructorTemplate) );
        }
    }

    [Template]
    private void OverrideConstructorTemplate()
    {
        Console.WriteLine( $"Executing constructor {meta.Target.Constructor}: started" );
        meta.Proceed();
        Console.WriteLine( $"Executing constructor {meta.Target.Constructor}: completed" );
    }
}