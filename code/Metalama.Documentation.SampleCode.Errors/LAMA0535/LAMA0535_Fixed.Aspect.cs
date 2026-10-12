// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0535.Fixed;

public class InitializationHookAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.OnInitialized) );
    }

    // Introduces a partial method that the user can implement in another part of the class.
#pragma warning disable CS0626
    [Template( IsPartial = true )]
    private extern void OnInitialized();
#pragma warning restore CS0626
}
