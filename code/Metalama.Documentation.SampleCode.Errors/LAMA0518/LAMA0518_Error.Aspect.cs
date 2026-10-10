// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0518.Error;

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

    // Error: the interface property is read-only, but the template has a setter.
    [InterfaceMember( IsExplicit = true )]
    public string Description { get => meta.Target.Type.Name; set { } }
}
