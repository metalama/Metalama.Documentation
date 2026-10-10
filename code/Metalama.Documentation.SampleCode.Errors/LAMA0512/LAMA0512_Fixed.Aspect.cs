// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0512.Fixed;

public interface IDescribable
{
    string Describe();
}

public class DescribableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // OverrideStrategy.Ignore skips types that already implement the interface.
        builder.ImplementInterface( typeof(IDescribable), OverrideStrategy.Ignore );
    }

    [InterfaceMember]
    public string Describe()
    {
        return meta.Target.Type.Name;
    }
}
