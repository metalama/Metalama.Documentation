// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0517.Fixed;

public interface ITrackable
{
    bool IsChanged { get; set; }
}

public class TrackableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.ImplementInterface( typeof(ITrackable) );
    }

    // Fixed: the template has both accessors of the interface property.
    [InterfaceMember( IsExplicit = true )]
    public bool IsChanged { get; set; }
}
