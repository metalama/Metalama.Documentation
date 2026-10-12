// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0531.Error;

public class MementoAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: the target type already has a nested type named Snapshot.
        builder.IntroduceClass( "Snapshot", buildType: type => type.Accessibility = Accessibility.Public );
    }
}
