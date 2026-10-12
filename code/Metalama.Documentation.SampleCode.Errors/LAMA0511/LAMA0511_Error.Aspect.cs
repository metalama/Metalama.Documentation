// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0511.Error;

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

    // The return type object doesn't match the return type string of IDescribable.Describe().
    [InterfaceMember]
    public object Describe()
    {
        return meta.Target.Type.Name;
    }
}
