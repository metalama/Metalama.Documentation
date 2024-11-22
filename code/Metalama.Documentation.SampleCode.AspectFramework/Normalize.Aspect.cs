// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.Normalize;

internal class NormalizeAttribute : FieldOrPropertyAspect
{
    public override void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
    {
        builder.Override( nameof(this.OverrideProperty) );
    }

    [Template]
    private string? OverrideProperty
    {
        set => meta.Target.FieldOrProperty.Value = value?.Trim().ToLowerInvariant();
    }
}