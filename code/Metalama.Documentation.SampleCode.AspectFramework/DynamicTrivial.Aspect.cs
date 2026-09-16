// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.DynamicTrivial;

internal class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        meta.This._logger.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}