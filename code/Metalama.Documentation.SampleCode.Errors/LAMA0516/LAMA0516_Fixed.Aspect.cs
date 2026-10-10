// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0516.Fixed;

public interface IDescribable
{
    string Describe();
}

public class DescribableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Fixed: OverrideStrategy.Ignore is supported for interfaces.
        builder.ImplementInterface( typeof(IDescribable), OverrideStrategy.Ignore );
    }

    [InterfaceMember]
    public string Describe() => meta.Target.Type.Name;
}
