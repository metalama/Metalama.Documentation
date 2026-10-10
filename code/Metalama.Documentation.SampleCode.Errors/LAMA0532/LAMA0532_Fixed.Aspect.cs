// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0532.Fixed;

public class ValidatableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Derived classes must implement the Validate method.
        builder.IntroduceMethod( nameof(this.Validate), buildMethod: method => method.IsAbstract = true );
    }

    // A template without a body introduces a method without a body.
#pragma warning disable CS0626
    [Template]
    public extern void Validate();
#pragma warning restore CS0626
}
