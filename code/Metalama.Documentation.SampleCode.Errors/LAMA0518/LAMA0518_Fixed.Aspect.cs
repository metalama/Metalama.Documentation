// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0518.Fixed;

public interface IDescribable
{
    string Description { get; }
}

public class DescribableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.ImplementInterface( typeof(IDescribable) );
    }

    // Fixed: the template has only the getter of the interface property.
    [InterfaceMember( IsExplicit = true )]
    public string Description { get => meta.Target.Type.Name; }
}
