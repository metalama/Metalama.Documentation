// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0519.Fixed;

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

    // Fixed: the member is public, so it can implement the interface implicitly.
    [InterfaceMember]
    public string Describe()
    {
        return $"This is an instance of {meta.Target.Type}.";
    }
}
