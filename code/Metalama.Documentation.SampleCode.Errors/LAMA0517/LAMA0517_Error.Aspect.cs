// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0517.Error;

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

    // Error: the interface property has a setter, but the template does not.
    [InterfaceMember( IsExplicit = true )]
    public bool IsChanged { get => false; }
}
