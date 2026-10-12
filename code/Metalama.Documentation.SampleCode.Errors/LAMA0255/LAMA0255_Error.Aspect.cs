// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0255.Error;

public class LogFirstParameterAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: Value is the run-time value of the parameter, so it cannot be cast to IParameter.
        _ = (IParameter) meta.Target.Parameters[0].Value!;

        return meta.Proceed();
    }
}
