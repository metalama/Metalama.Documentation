// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0511.Fixed;

public interface IDescribable
{
    string Describe();
}

public class DescribableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.ImplementInterface( typeof(IDescribable) );
    }

    // The return type matches the return type of IDescribable.Describe().
    [InterfaceMember]
    public string Describe()
    {
        return meta.Target.Type.Name;
    }
}
