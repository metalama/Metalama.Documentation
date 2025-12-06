// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System;
using System.Linq;

namespace Doc.SuppressWarningWithFilter;

internal class SuppressUnusedVariableAttribute : OverrideMethodAspect
{
    private static readonly SuppressionDefinition _suppressUnusedVariable = new( "CS0219" );

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        // Suppress "variable is assigned but never used" only for variables named "_initialized".
        // Other CS0219 warnings in the same scope will still be reported.
        builder.Diagnostics.Suppress(
            _suppressUnusedVariable.WithFilter(
                d => d.Arguments.Any( a => a is string s && s == "_initialized" ) ),
            builder.Target );
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method.Name}" );

        return meta.Proceed();
    }
}
