// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0516.Error;

public interface IDescribable
{
    string Describe();
}

public class DescribableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: OverrideStrategy.New is not supported for interfaces.
        builder.ImplementInterface( typeof(IDescribable), OverrideStrategy.New );
    }

    [InterfaceMember]
    public string Describe() => meta.Target.Type.Name;
}
