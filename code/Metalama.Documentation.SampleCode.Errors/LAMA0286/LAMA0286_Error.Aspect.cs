// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0286.Error;

public class LogStartTimeAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        object startTime = DateTime.Now;
        IExpression startTimeExpression;

        // Error: 'startTime' is a run-time expression that isn't dynamic, so it can't be cast to IExpression.
        startTimeExpression = (IExpression) startTime;

        Console.WriteLine( $"{meta.Target.Method.Name} started at {startTimeExpression.Value}." );

        return meta.Proceed();
    }
}
