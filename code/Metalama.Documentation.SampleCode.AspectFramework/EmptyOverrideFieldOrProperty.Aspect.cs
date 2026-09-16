// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc;

public class EmptyOverrideFieldOrPropertyAttribute : OverrideFieldOrPropertyAspect
{
    public override dynamic? OverrideProperty
    {
        get => meta.Proceed();
        set => meta.Proceed();
    }
}