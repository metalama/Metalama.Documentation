// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0519.Error;

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

    // Error: an implicit interface implementation must be public.
    [InterfaceMember]
    private string Describe()
    {
        return $"This is an instance of {meta.Target.Type}.";
    }
}
