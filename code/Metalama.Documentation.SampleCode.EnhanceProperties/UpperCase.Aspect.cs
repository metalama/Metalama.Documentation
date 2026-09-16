// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.UpperCase;

/// <summary>
/// Changes the value of a string property to Upper Case
/// </summary>
public class UpperCaseAttribute : OverrideFieldOrPropertyAspect
{
    public override dynamic? OverrideProperty
    {
        get => meta.Proceed();
        set => meta.Target.FieldOrProperty.Value = value?.ToUpper();
    }
}