// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.Trimmed;

public class TrimAttribute : OverrideFieldOrPropertyAspect
{
    public override dynamic? OverrideProperty
    {
        get => meta.Proceed();
        set => meta.Target.FieldOrProperty.Value = value?.Trim();
    }
}