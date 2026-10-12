// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;

namespace Doc.LAMA0286.Fixed;

public class LogStartTimeAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        object startTime = DateTime.Now;
        IExpression startTimeExpression;

        // Fixed: ExpressionFactory.Capture creates an IExpression from a run-time expression.
        startTimeExpression = ExpressionFactory.Capture( startTime );

        Console.WriteLine( $"{meta.Target.Method.Name} started at {startTimeExpression.Value}." );

        return meta.Proceed();
    }
}
