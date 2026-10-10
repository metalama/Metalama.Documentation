// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;

namespace Doc.LAMA0233.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method} on {GetTarget().Value}." );

        return meta.Proceed();
    }

    // Compile-time helper that returns the type name for static methods, and the current instance otherwise.
    private static IExpression GetTarget()
        => meta.Target.Method.IsStatic
            ? ExpressionFactory.Literal( meta.Target.Type.Name )
            : meta.This; // Error: meta.This can only be used inside a template.
}
