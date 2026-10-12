// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0533.Error;

public class ValidatableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: an abstract method can't be introduced with OverrideStrategy.Override.
        builder.IntroduceMethod( nameof(this.Validate), whenExists: OverrideStrategy.Override );
    }

    // A template without a body introduces an abstract method into an abstract class.
#pragma warning disable CS0626
    [Template]
    public extern void Validate();
#pragma warning restore CS0626
}
