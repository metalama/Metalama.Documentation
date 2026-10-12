// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0512.Error;

public interface IDescribable
{
    string Describe();
}

public class DescribableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The default strategy fails when the type already implements the interface.
        builder.ImplementInterface( typeof(IDescribable) );
    }

    [InterfaceMember]
    public string Describe()
    {
        return meta.Target.Type.Name;
    }
}
