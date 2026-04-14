// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.RaiseEvent;

public class RaiseStatusChangedAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var result = meta.Proceed();

        // Raise the StatusChanged event after the method executes.
        meta.Target.Type.Events["StatusChanged"].Raise( meta.This, EventArgs.Empty );

        return result;
    }
}
