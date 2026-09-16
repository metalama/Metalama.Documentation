// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc;

public class EmptyOverrideMethodAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        return meta.Proceed();
    }
}