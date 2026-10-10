// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0531.Fixed;

public class MementoAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Fixed: the introduced type has a name that doesn't conflict with the existing nested type.
        builder.IntroduceClass( "Memento", buildType: type => type.Accessibility = Accessibility.Public );
    }
}
