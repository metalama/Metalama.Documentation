// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0514.Fixed;

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

    // The existing Describe method of the target type implements the interface member.
    [InterfaceMember( WhenExists = InterfaceMemberOverrideStrategy.Ignore )]
    public string Describe() => meta.Target.Type.Name;
}
